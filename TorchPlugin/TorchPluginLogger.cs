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
        public void Trace(string message, object[] data, Exception ex = null)
        {
            if (!logger.IsTraceEnabled)
                return;

            logger.Trace(Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Debug(string message, object[] data, Exception ex = null)
        {
            if (!logger.IsDebugEnabled)
                return;

            logger.Debug(Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Info(string message, object[] data, Exception ex = null)
        {
            if (!logger.IsInfoEnabled)
                return;

            logger.Info(Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Warning(string message, object[] data, Exception ex = null)
        {
            if (!logger.IsWarnEnabled)
                return;

            logger.Warn(Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Error(string message, object[] data, Exception ex = null)
        {
            if (!logger.IsErrorEnabled)
                return;

            logger.Error(Format(message, data, ex));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Critical(string message, object[] data, Exception ex = null)
        {
            if (!logger.IsFatalEnabled)
                return;

            logger.Fatal(Format(message, data, ex));
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