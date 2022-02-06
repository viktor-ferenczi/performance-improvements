using System;
using System.Runtime.CompilerServices;
using NLog;
using Shared.Logging;

namespace TorchPlugin
{
    public class PluginLogger : LogFormatter, IPluginLogger
    {
        private readonly Logger logger;

        public PluginLogger(string pluginName) : base("")
        {
            logger = LogManager.GetLogger(pluginName);
        }

        public bool IsTraceEnabled => logger.IsTraceEnabled;
        public bool IsDebugEnabled => logger.IsDebugEnabled;
        public bool IsInfoEnabled => logger.IsInfoEnabled;
        public bool IsWarningEnabled => logger.IsWarnEnabled;
        public bool IsErrorEnabled => logger.IsErrorEnabled;
        public bool IsCriticalEnabled => logger.IsFatalEnabled;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trace(Exception ex, string message, params object[] data)
        {
            if (!IsTraceEnabled)
                return;

            logger.Trace(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(Exception ex, string message, params object[] data)
        {
            if (!IsDebugEnabled)
                return;

            logger.Debug(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(Exception ex, string message, params object[] data)
        {
            if (!IsInfoEnabled)
                return;

            logger.Info(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(Exception ex, string message, params object[] data)
        {
            if (!IsWarningEnabled)
                return;

            logger.Warn(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(Exception ex, string message, params object[] data)
        {
            if (!IsErrorEnabled)
                return;

            logger.Error(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(Exception ex, string message, params object[] data)
        {
            if (!IsCriticalEnabled)
                return;

            logger.Fatal(Format(ex, message, data));
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