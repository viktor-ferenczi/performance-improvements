using System;
using System.Diagnostics;
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
        private static readonly long Millisecond = Stopwatch.Frequency / 1000;
        private static readonly long MaxBusyWaitDuration = Millisecond / 2;

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
        private static bool SpinOncePrefix(ref int ___m_count, ref long ___m_startTime, ref long ___m_startTimeLong)
        {
            var config = Config;
            if (!config.Enabled || !config.FixSpinWait)
                return true;

            spin.Increment();

            if (!Multiprocessor)
            {
                yield.Increment();
                Thread.Yield();
                return false;
            }

            if (___m_count++ == 0)
            {
                ___m_startTime = Stopwatch.GetTimestamp();
                ___m_startTimeLong = ___m_startTime + MaxBusyWaitDuration;
                wait.Increment();
                return false;
            }

            var now = Stopwatch.GetTimestamp();
            if (now >= ___m_startTimeLong)
            {
                yield.Increment();
                if (Thread.Yield())
                    return false;
            }

            sleep.Increment();
            Thread.Sleep(1);
            return false;
        }
    }
}