using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using System.Threading;
using ParallelTasks;

namespace ClientPlugin.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MySpinWait))]
    public static class MySpinWaitPatch
    {
        private static readonly bool Multiprocessor = Environment.ProcessorCount > 1;
        private static long waitCount;
        private static long totalSpinCount;
        private static int maxSpinCount;

        public static void LogStatistics(int period)
        {
            if (Plugin.Tick % period != 0 || waitCount == 0)
                return;

            // There can be some minimal inconsistency, that's okay for logging purposes
            var statWaitCount = Interlocked.Exchange(ref waitCount, 0);
            var statTotalSpinCount = Interlocked.Exchange(ref totalSpinCount, 0);
            var statMaxSpinCount = Interlocked.Exchange(ref maxSpinCount, 0);

            var seconds = period / 60;
            Plugin.Log.Debug("SpinWait: {0} waits/second, {1} spins/second, {2} spins/wait, max {3} spins/wait",
                statWaitCount / seconds,
                statTotalSpinCount / seconds,
                statTotalSpinCount / statWaitCount,
                statMaxSpinCount);
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MySpinWait.SpinOnce))]
        private static bool SpinOncePrefix(ref int ___m_count)
        {
            if (___m_count == 0)
                Interlocked.Increment(ref waitCount);

            Interlocked.Increment(ref totalSpinCount);

            var spinCount = ___m_count < 1_000_000_000 ? ++___m_count : ___m_count;

            var oldMaxSpinCount = maxSpinCount;
            if (spinCount > oldMaxSpinCount)
                Interlocked.CompareExchange(ref maxSpinCount, spinCount, oldMaxSpinCount);

            if (Multiprocessor)
            {
                if (spinCount < 6)
                {
                    BusyWait(1 << spinCount);
                    return false;
                }
            }

            if (!Thread.Yield())
                Thread.Sleep(1);

            return false;
        }

        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        private static void BusyWait(int count)
        {
            // ReSharper disable once EmptyForStatement
            for (var i = 1; i < count; i++)
            {
            }
        }
    }
}