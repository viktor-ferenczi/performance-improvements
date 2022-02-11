using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using System.Threading;
using ParallelTasks;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MySpinWait))]
    public static class MySpinWaitPatch
    {
        private static IPluginLogger Log => Common.Logger;
        private static IPluginConfig Config => Common.Config;

        private static Stats wait;
        private static Stats spin;
        private static Stats yield;
        private static Stats sleep;

        private static readonly bool Multiprocessor = Environment.ProcessorCount > 1;

        public static void LogStats(long tick, int period)
        {
            var config = Config;
            if (!config.Enabled || !config.FixSpinWait)
                return;

            if (tick % period != 0 ||
                !Log.IsInfoEnabled ||
                wait.Count == 0)
                return;

            var seconds = period / 60;
            var waits = wait.Count;

            // There can be some minimal inconsistency, but that's okay for logging purposes
            Log.Info("SpinWait: wait {0}; spin {1}; yield {2}; sleep {3}",
                wait.Format(seconds),
                spin.Format(seconds, waits),
                yield.Format(seconds, waits),
                sleep.Format(seconds, waits));

            wait.Reset();
            spin.Reset();
            yield.Reset();
            sleep.Reset();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MySpinWait.SpinOnce))]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static bool SpinOncePrefix(ref int ___m_count, ref long ___m_startTime)
        {
            var config = Config;
            if (!config.Enabled || !config.FixSpinWait)
                return true;

            if (___m_count == 0)
            {
                wait.Increment();
                ___m_startTime = 0;
            }

            spin.Increment();

            var count = ++___m_count;
            spin.UpdateMax(count);

            long yields;
            if (Multiprocessor)
            {
                if (count < 10)
                {
                    BusyWait(1 << (int)count);
                    return false;
                }

                yields = count - 9;
            }
            else
            {
                yields = count;
            }

            yield.Increment();
            yield.UpdateMax(yields);
            if (!Thread.Yield())
            {
                sleep.Increment();
                sleep.UpdateMax(++___m_startTime);
                Thread.Sleep(1);
            }

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