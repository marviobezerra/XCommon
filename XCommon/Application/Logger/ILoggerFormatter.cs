using System;

namespace XCommon.Application.Logger
{
    public interface ILoggerFormatter
    {
        string Format(LogType type, string message, params object[] args);

        string Format(LoggerInfo info, LogType type, string message, params object[] args);
    }
}
