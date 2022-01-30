using System;
using System.Runtime.CompilerServices;
using Shared.Logging;
using VRage.Utils;

namespace Shared.Logging
{
    public class KeenPluginLogger : LogFormatter, IPluginLogger
    {
        public KeenPluginLogger(string pluginName) : base($"{pluginName}: ")
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trace(string message, object[] data, Exception ex = null)
        {
            // Keen does not have a Trace log level, using Debug instead
            Debug(message, data, ex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(string message, object[] data, Exception ex = null)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Debug, Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(string message, object[] data, Exception ex = null)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Info, Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(string message, object[] data, Exception ex = null)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Warning, Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(string message, object[] data, Exception ex = null)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Error, Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(string message, object[] data, Exception ex = null)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Critical, Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trace(string message, Exception ex = null)
        {
            Trace(message, null, ex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(string message, Exception ex = null)
        {
            Debug(message, null, ex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(string message, Exception ex = null)
        {
            Info(message, null, ex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(string message, Exception ex = null)
        {
            Warning(message, null, ex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(string message, Exception ex = null)
        {
            Error(message, null, ex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(string message, Exception ex = null)
        {
            Critical(message, null, ex);
        }
    }
}