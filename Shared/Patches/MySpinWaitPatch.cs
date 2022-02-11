using System;
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
        private static bool SpinOncePrefix(
            // ReSharper disable once InconsistentNaming
            ref int ___m_count,
            // ReSharper disable once InconsistentNaming
            ref long ___m_startTime)
        {
            var config = Config;
            if (!config.Enabled || !config.FixSpinWait)
                return true;

            if (___m_startTime == 0)
                wait.Increment();

            spin.Increment();

            var count = ++___m_startTime;
            spin.UpdateMax(count);

            long yields;
            if (Multiprocessor)
            {
                if (count < 6)
                {
                    BusyWait(1 << (int)count);
                    return false;
                }

                yields = count - 5;
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
                sleep.UpdateMax(++___m_count);
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