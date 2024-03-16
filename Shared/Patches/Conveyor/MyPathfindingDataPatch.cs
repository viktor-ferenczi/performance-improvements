// New version based on ThreadLocal, but it is not faster
// than the original (about the same performance)
#if DISABLED

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;
using Shared.Tools;
using VRage.Algorithms;

namespace Shared.Patches
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
            ___m_lockObject = new ThreadLocal<long>(() => 0L);
        }

        [HarmonyPrefix]
        [HarmonyPatch("Timestamp", MethodType.Setter)]
        [EnsureCode("af65d019")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TimestampSetterPrefix(object ___m_lockObject, long value)
        {
            ((ThreadLocal<long>)___m_lockObject).Value = value;
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Timestamp", MethodType.Getter)]
        [EnsureCode("8e608197")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TimestampGetterPrefix(object ___m_lockObject, out long __result)
        {
            __result = ((ThreadLocal<long>)___m_lockObject).Value;
            return false;
        }
    }
}

#endif