using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;
using Shared.Tools;
using VRage.Algorithms;

namespace Shared.Patches.Conveyor
{
    [HarmonyPatch(typeof(MyPathfindingData))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyPathfindingDataPatch
    {
        // These methods are called a lot, therefore this fix cannot be disabled.
        // FIXME: Turn them into transpiler patches and add configurability.

        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, typeof(object))]
        [EnsureCode("7badc1eb")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void TimestampCtorPostfix(out object ___m_lockObject)
        {
            ___m_lockObject = new RwLockCounter();
        }

        [HarmonyPrefix]
        [HarmonyPatch("Timestamp", MethodType.Setter)]
        [EnsureCode("af65d019")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TimestampSetterPrefix(object ___m_lockObject, Dictionary<Thread, long> ___threadedTimestamp, long value)
        {
            if (!(___m_lockObject is RwLockCounter rwLock))
            {
                // ReSharper disable once RedundantAssignment
                ___m_lockObject = rwLock = new RwLockCounter();
            }

            rwLock.AcquireForWriting();
            ___threadedTimestamp[Thread.CurrentThread] = value;
            rwLock.ReleaseAfterWriting();

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Timestamp", MethodType.Getter)]
        [EnsureCode("8e608197")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TimestampGetterPrefix(object ___m_lockObject, Dictionary<Thread, long> ___threadedTimestamp, out long __result)
        {
            if (!(___m_lockObject is RwLockCounter rwLock))
            {
                // ReSharper disable once RedundantAssignment
                ___m_lockObject = rwLock = new RwLockCounter();
            }

            rwLock.AcquireForReading();
            ___threadedTimestamp.TryGetValue(Thread.CurrentThread, out __result);
            rwLock.ReleaseAfterReading();

            return false;
        }
    }
}