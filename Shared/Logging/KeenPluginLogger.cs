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
        public void Trace(Exception ex, string message, params object[] data)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            // Keen does not have a Trace log level, using Debug instead
            MyLog.Default.Log(MyLogSeverity.Debug, Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(Exception ex, string message, params object[] data)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Debug, Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(Exception ex, string message, params object[] data)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Info, Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(Exception ex, string message, params object[] data)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Warning, Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(Exception ex, string message, params object[] data)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Error, Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(Exception ex, string message, params object[] data)
        {
            if (!MyLog.Default.LogEnabled)
                return;

            MyLog.Default.Log(MyLogSeverity.Critical, Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trace(string message, params object[] data)
        {
            Trace(null, message, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(string message, params object[] data)
        {
            Debug(null, message, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(string message, params object[] data)
        {
            Info(null, message, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(string message, params object[] data)
        {
            Warning(null, message, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(string message, params object[] data)
        {
            Error(null, message, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(string message, params object[] data)
        {
            Critical(null, message, data);
        }
    }
}