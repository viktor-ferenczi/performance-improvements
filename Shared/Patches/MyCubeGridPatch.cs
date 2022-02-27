using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Config;
using Shared.Plugin;

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

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.Dispatch))]
        private static bool DispatchPrefix(MyCubeGrid __instance, MyCubeGrid.UpdateQueue queue, bool parallel, ref MyCubeGrid.UpdateQueue ___m_updateInProgress, ref List<MyCubeGrid.Update>[] ___m_updateQueues, ref int ___m_totalQueuedParallelUpdates, ref int ___m_totalQueuedSynchronousUpdates)
        {
            if (!Config.Enabled || !Config.FixGridDispatch)
                return true;

            lock (___m_updateQueues)
            {
                if (___m_updateInProgress != MyCubeGrid.UpdateQueue.Invalid)
                    throw new InvalidOperationException("An update queue is already being dispatched for this entity.");

                ___m_updateInProgress = queue;
            }

            var queueIndex = (int)queue - 1;
            var updateList = ___m_updateQueues[queueIndex];
            if (updateList == null)
            {
                updateList = new List<MyCubeGrid.Update>(16);
                ___m_updateQueues[queueIndex] = updateList;
            }

            var isExecutedOnce = queue.IsExecutedOnce();

            var num = 0;
            var par = 0;

            var cnt = updateList.Count;
            for (var index = 0; index < cnt; ++index)
            {
                MyCubeGrid.Update u = updateList[index];
                var isParallel = u.Parallel == parallel;

                if (isParallel && !u.Removed)
                    __instance.Invoke(in u, queue);

                if (u.Removed || isExecutedOnce && isParallel)
                {
                    ++num;
                    if (u.Parallel)
                        par++;
                }
                else if (num > 0)
                {
                    updateList[index - num] = u;
                    updateList[index] = MyCubeGrid.Update.Empty;
                }
            }

            if (num != 0)
            {
                if (par > 0)
                    Interlocked.Add(ref ___m_totalQueuedParallelUpdates, -par);

                var syn = num - par;
                if (syn > 0)
                    Interlocked.Add(ref ___m_totalQueuedSynchronousUpdates, -syn);

                updateList.RemoveRange(cnt - num, num);
            }

            __instance.EndUpdate();

            return false;
        }
    }
}