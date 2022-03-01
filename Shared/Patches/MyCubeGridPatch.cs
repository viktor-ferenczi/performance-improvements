using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;
using Priority_Queue;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Config;
using Shared.Plugin;
using VRage.ModAPI;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCubeGrid))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MyCubeGridPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsInMergeGridInternal => CallDepth.Value > 0;

        #region MergeGridInternal

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.MergeGridInternal))]
        private static bool MergeGridInternalPrefix()
        {
            if (!Config.Enabled || !Config.FixGridMerge)
                return true;

            CallDepth.Value++;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeGrid.MergeGridInternal))]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance)
        {
            if (!IsInMergeGridInternal)
                return;

            if (--CallDepth.Value > 0)
                return;

            // Update the conveyor system only after the merge is complete
            __instance.GridSystems.ConveyorSystem.FlagForRecomputation();
        }

        #endregion

        #region PasteBlocksServer

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.PasteBlocksServer))]
        private static bool PasteBlocksServerPrefix(ref bool? __state)
        {
            if (!Config.Enabled || !Config.FixGridPaste)
                return true;

            // Disable updates for the duration of the paste,
            // it eliminates most spin lock contention
            __state = MySession.Static.m_updateAllowed;
            MySession.Static.m_updateAllowed = false;
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeGrid.PasteBlocksServer))]
        private static void PasteBlocksServerPostfix(bool? __state)
        {
            if (__state == null)
                return;

            MySession.Static.m_updateAllowed = (bool)__state;
        }

        #endregion

        #region Update scheduling and dispatch

        public class GridUpdateQueues
        {
            public const int UpdateQueueCount = 5;

            public SimplePriorityQueue<Action>[] ParallelQueues = new SimplePriorityQueue<Action>[UpdateQueueCount];
            public SimplePriorityQueue<Action>[] SynchronousQueues = new SimplePriorityQueue<Action>[UpdateQueueCount];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public GridUpdateQueues()
            {
                for (var i = 1; i < UpdateQueueCount; i++)
                {
                    ParallelQueues[i] = new SimplePriorityQueue<Action>();
                    SynchronousQueues[i] = new SimplePriorityQueue<Action>();
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Clear()
            {
                for (var i = 1; i < UpdateQueueCount; i++)
                {
                    ParallelQueues[i].Clear();
                    SynchronousQueues[i].Clear();
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool EnqueueParallel(MyCubeGrid.UpdateQueue queue, Action callback, int priority)
            {
                var parallelQueue = ParallelQueues[(int)queue];
                if (parallelQueue == null)
                    return false;

                if (parallelQueue.Contains(callback))
                    return false;

                parallelQueue.Enqueue(callback, priority);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool EnqueueSynchronous(MyCubeGrid.UpdateQueue queue, Action callback, int priority)
            {
                var synchronousQueue = SynchronousQueues[(int)queue];
                if (synchronousQueue == null)
                    return false;

                if (synchronousQueue.Contains(callback))
                    return false;

                synchronousQueue.Enqueue(callback, priority);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool RemoveParallel(MyCubeGrid.UpdateQueue queue, Action callback)
            {
                var parallelQueue = ParallelQueues[(int)queue];
                if (!parallelQueue.Contains(callback))
                    return false;

                parallelQueue.Remove(callback);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool RemoveSynchronous(MyCubeGrid.UpdateQueue queue, Action callback)
            {
                var synchronousQueue = SynchronousQueues[(int)queue];
                if (!synchronousQueue.Contains(callback))
                    return false;

                synchronousQueue.Remove(callback);
                return true;
            }
        }

        private static Dictionary<MyCubeGrid, GridUpdateQueues> gridUpdateQueues = new Dictionary<MyCubeGrid, GridUpdateQueues>();

        private static List<GridUpdateQueues> freeGridUpdateQueues = new List<GridUpdateQueues>();

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.GetDebugUpdateInfo))]
        private static bool GetDebugUpdateInfoPrefix(MyCubeGrid __instance, List<MyCubeGrid.DebugUpdateRecord> gridDebugUpdateInfo)
        {
            return false;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.Schedule))]
        private static bool SchedulePrefix(
            MyCubeGrid __instance,
            MyCubeGrid.UpdateQueue queue,
            Action callback,
            int priority,
            bool parallel,
            int ___m_totalQueuedParallelUpdates,
            int ___m_totalQueuedSynchronousUpdates)
        {
            lock (gridUpdateQueues)
            {
                if (gridUpdateQueues.TryGetValue(__instance, out var queues))
                {
                    if (parallel)
                    {
                        if (queues.EnqueueParallel(queue, callback, priority))
                        {
                            if (Interlocked.Increment(ref ___m_totalQueuedParallelUpdates) == 1 && __instance.InScene)
                                MyEntities.Orchestrator.EntityFlagsChanged(__instance);
                        }
                    }
                    else
                    {
                        if (queues.EnqueueSynchronous(queue, callback, priority))
                        {
                            if (Interlocked.Increment(ref ___m_totalQueuedSynchronousUpdates) == 1)
                                __instance.NeedsUpdate |= MyEntityUpdateEnum.EACH_FRAME;
                        }
                    }
                }
            }

            return false;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.DeSchedule))]
        private static bool DeSchedulePrefix(
            MyCubeGrid __instance,
            MyCubeGrid.UpdateQueue queue,
            Action callback,
            int ___m_totalQueuedParallelUpdates,
            int ___m_totalQueuedSynchronousUpdates)
        {
            lock (gridUpdateQueues)
            {
                if (gridUpdateQueues.TryGetValue(__instance, out var queues))
                {
                    if (queues.RemoveParallel(queue, callback))
                    {
                        if (Interlocked.Decrement(ref ___m_totalQueuedParallelUpdates) == 0 && __instance.InScene)
                            MyEntities.Orchestrator.EntityFlagsChanged(__instance);
                    }

                    if (queues.RemoveSynchronous(queue, callback))
                    {
                        if (Interlocked.Decrement(ref ___m_totalQueuedSynchronousUpdates) == 0)
                            __instance.NeedsUpdate &= ~MyEntityUpdateEnum.EACH_FRAME;
                    }
                }
            }

            return false;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.Dispatch))]
        private static bool DispatchPrefix(
            MyCubeGrid __instance,
            MyCubeGrid.UpdateQueue queue,
            bool parallel,
            ref int ___m_totalQueuedParallelUpdates,
            ref int ___m_totalQueuedSynchronousUpdates)
        {
            GridUpdateQueues queues;
            lock (gridUpdateQueues)
            {
                if (!gridUpdateQueues.TryGetValue(__instance, out queues))
                    return false;
            }

            if (queues == null)
                return false;

            var q = parallel ? queues.ParallelQueues[(int)queue] : queues.SynchronousQueues[(int)queue];
            if (q == null)
                return false;

            if (queue.IsExecutedOnce())
            {
                var count = q.Count;
                while (q.Count != 0)
                    q.Dequeue().Invoke();

                if (parallel)
                {
                    if (Interlocked.Add(ref ___m_totalQueuedParallelUpdates, -count) == 0 && __instance.InScene)
                        MyEntities.Orchestrator.EntityFlagsChanged(__instance);
                }
                else
                {
                    if (Interlocked.Add(ref ___m_totalQueuedSynchronousUpdates, -count) == 0)
                        __instance.NeedsUpdate &= ~MyEntityUpdateEnum.EACH_FRAME;
                }
            }
            else
            {
                foreach (var action in q)
                    action.Invoke();
            }

            return false;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeGrid.Init))]
        private static void InitPostfix(MyCubeGrid __instance)
        {
            lock (freeGridUpdateQueues)
            {
                gridUpdateQueues[__instance] = freeGridUpdateQueues.Count == 0 ? new GridUpdateQueues() : freeGridUpdateQueues.Pop();
            }
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeGrid.BeforeDelete))]
        private static void BeforeDeletePostfix(MyCubeGrid __instance)
        {
            lock (gridUpdateQueues)
            {
                if (gridUpdateQueues.TryGetValue(__instance, out var queues))
                {
                    gridUpdateQueues.Remove(__instance);
                    queues.Clear();
                    lock (freeGridUpdateQueues)
                    {
                        freeGridUpdateQueues.Add(queues);
                    }
                }
            }
        }

        #endregion
    }
}