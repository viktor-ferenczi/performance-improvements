using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Shared.Tools
{
    // Not thread safe, since we don't care about super exact results
    public class ConveyorStat
    {
        private long CallCount { get; set; }
        private long FailureCount { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Reset();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            CallCount = 0;
            FailureCount = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CountCall()
        {
            CallCount++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CountFailure()
        {
            FailureCount++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Add(ConveyorStat source)
        {
            CallCount += source.CallCount;
            FailureCount += source.FailureCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(IEnumerable<ConveyorStat> stats)
        {
            foreach (var stat in stats)
                Add(stat);
        }

        public string FullReport(int period)
        {
            var seconds = Math.Max(1, period) / 60.0;

            var callCount = CallCount;
            var callFrequency = callCount / seconds;
            var failureCount = FailureCount;
            var failureRate = (double)failureCount / Math.Max(1, callCount);

            Reset();

            return $"Count = {callCount} ({callFrequency:F1}/s); Failed = {failureCount} ({100.0 * failureRate:F2}%)";
        }

        public string CountReport(int period)
        {
            var seconds = Math.Max(1, period) / 60.0;

            var callCount = CallCount;
            var callFrequency = callCount / seconds;

            Reset();

            return $"Count = {callCount} ({callFrequency:F1}/s)";
        }
    }
}