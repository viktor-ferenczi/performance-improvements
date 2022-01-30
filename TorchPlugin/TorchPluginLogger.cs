using System;
using System.Runtime.CompilerServices;
using NLog;
using Shared.Logging;

namespace TorchPlugin
{
    public class TorchPluginLogger : LogFormatter, IPluginLogger
    {
        private readonly Logger logger;

        public TorchPluginLogger(string pluginName) : base("")
        {
            logger = LogManager.GetLogger(pluginName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trace(Exception ex, string message, params object[] data)
        {
            if (!logger.IsTraceEnabled)
                return;

            logger.Trace(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(Exception ex, string message, params object[] data)
        {
            if (!logger.IsDebugEnabled)
                return;

            logger.Debug(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(Exception ex, string message, params object[] data)
        {
            if (!logger.IsInfoEnabled)
                return;

            logger.Info(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(Exception ex, string message, params object[] data)
        {
            if (!logger.IsWarnEnabled)
                return;

            logger.Warn(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(Exception ex, string message, params object[] data)
        {
            if (!logger.IsErrorEnabled)
                return;

            logger.Error(Format(ex, message, data));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(Exception ex, string message, params object[] data)
        {
            if (!logger.IsFatalEnabled)
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