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
            LoggerData = Writter.LoadData();
        }

        [Inject]
        private IApplicationSettings ApplicationSettings { get; set; }

        [Inject]
        public ILoggerWritter Writter { get; set; }

        public List<LoggerEntity> LoggerData { get; set; }
        
        public async Task<ILogger> DebugAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync<object>(LogType.Debug, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> DebugAsync<TResource>(TResource resource, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(LogType.Debug, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync<object>(LogType.Error, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ErrorAsync<TResource>(TResource resource, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(LogType.Error, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync<object>(LogType.Exception, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync(Exception exception, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync<object>(LogType.Exception, null, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> ExceptionAsync<TResource>(Exception exception, TResource resource, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(LogType.Exception, resource, exception, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync<object>(LogType.Info, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> InfoAsync<TResource>(TResource resource, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(LogType.Info, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync<object>(LogType.Trace, null, null, message, memberName, sourceFilePath, sourceLineNumber);

        public async Task<ILogger> TraceAsync<TResource>(TResource resource, string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
            => await WriteAsync(LogType.Trace, resource, null, message, memberName, sourceFilePath, sourceLineNumber);

        private async Task<ILogger> WriteAsync<TResource>(LogType type, TResource resource, Exception exception, string message, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            if (ApplicationSettings.Logger < type)
                return this;

            await Task.Factory.StartNew(() =>
            {
                LoggerData.Add(new LoggerEntity(exception)
                {
                    Type = type,
                    Resource = resource,
                    Message = message,
                    MemberName = memberName,
                    SourceFilePath = sourceFilePath,
                    SourceLineNumber = sourceLineNumber
                });

                Writter.SaveData(LoggerData);
            });

            return this;
        }
    }
}
