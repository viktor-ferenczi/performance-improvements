using System;

namespace Shared.Logging
{
    public interface IPluginLogger
    {
        void Trace(string message, object[] data, Exception ex = null);
        void Debug(string message, object[] data, Exception ex = null);
        void Info(string message, object[] data, Exception ex = null);
        void Warning(string message, object[] data, Exception ex = null);
        void Error(string message, object[] data, Exception ex = null);
        void Critical(string message, object[] data, Exception ex = null);

        void Trace(string message, Exception ex = null);
        void Debug(string message, Exception ex = null);
        void Info(string message, Exception ex = null);
        void Warning(string message, Exception ex = null);
        void Error(string message, Exception ex = null);
        void Critical(string message, Exception ex = null);
    }
}