using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XCommon.Patterns.Ioc;

namespace XCommon.Application.Logger.Implementations
{
    public class Logger : ILogger
    {
        public Logger()
        {
            Kernel.Resolve(this);
        }

        [Inject]
        private IApplicationSettings ApplicationSettings { get; set; }

        [Inject]
        public ILoggerWritter Writter { get; set; }

        public List<LoggerEntity> LoggerData
        {
            get
            {
                return Writter.LoadDataAsync().Result;
            }
        }
        
        public async Task<ILogger> TraceAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, 0, LogType.Trace, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Trace, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Trace, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync<TCaller>(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), 0, LogType.Trace, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync<TCaller>(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Trace, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync<TCaller>(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Trace, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, 0, LogType.Info, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Info, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Info, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync<TCaller>(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), 0, LogType.Info, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync<TCaller>(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Info, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync<TCaller>(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Info, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, 0, LogType.Debug, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Debug, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Debug, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync<TCaller>(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), 0, LogType.Debug, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync<TCaller>(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Debug, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync<TCaller>(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Debug, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, 0, LogType.Error, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Error, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Error, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync<TCaller>(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), 0, LogType.Error, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync<TCaller>(string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Error, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync<TCaller>(string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Error, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
           => await WriteAsync(null, 0, LogType.Exception, null, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync(Exception exception, string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Exception, null, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync(Exception exception, string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(null, eventId, LogType.Exception, resource, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync<TCaller>(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), 0, LogType.Exception, null, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync<TCaller>(Exception exception, string message, int eventId, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Exception, null, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync<TCaller>(Exception exception, string message, int eventId, object resource, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(typeof(TCaller), eventId, LogType.Exception, resource, exception, message, memberName, sourceFilePath, sourceLineNumber);

        private async Task<ILogger> WriteAsync(Type callerType, int eventId, LogType type, object resource, Exception exception, string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (ApplicationSettings.Logger == LogType.None || ApplicationSettings.Logger > type)
                return this;

            await Writter.SaveDataAsync(new LoggerEntity(exception, callerType)
            {
                LogLevel = type,
                Resource = resource,
                Message = message,
                SourceMethod = memberName,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber
            });

            return this;
        }
    }
}
