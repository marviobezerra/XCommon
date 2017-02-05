using FluentAssertions;
using System;
using System.Threading.Tasks;
using XCommon.Application;
using XCommon.Application.Logger;
using XCommon.Application.Logger.Implementations;
using XCommon.Patterns.Ioc;
using Xunit;
using AppLogger = XCommon.Application.Logger.Implementations;

namespace XCommon.Test.Application.Logger
{
    public class LoggerTest
    {
        public ApplicationSettings AppSettings { get; set; }

        public LoggerTest()
        {
            AppSettings = new ApplicationSettings();

            Kernel.Map<IApplicationSettings>().To(AppSettings);
            Kernel.Map<ILoggerWritter>().To<LoggerWritterInMemory>();
            Kernel.Map<ILogger>().To<AppLogger.Logger>();
        }

        [Fact(DisplayName = "Logger (Level None)")]
        public async Task LogNone()
        {
            AppSettings.Logger = LogType.None;

            ILogger logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Logger (Level Trace)")]
        public async Task LogTrace()
        {
            AppSettings.Logger = LogType.Trace;

            ILogger logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(5);
        }

        [Fact(DisplayName = "Logger (Level Info)")]
        public async Task LogInfo()
        {
            AppSettings.Logger = LogType.Info;

            ILogger logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(4);
        }

        [Fact(DisplayName = "Logger (Level Debugger)")]
        public async Task LogDebugger()
        {
            AppSettings.Logger = LogType.Debug;

            ILogger logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(3);
        }

        [Fact(DisplayName = "Logger (Level Error)")]
        public async Task LogError()
        {
            AppSettings.Logger = LogType.Error;

            ILogger logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(2);
        }

        [Fact(DisplayName = "Logger (Level Exception)")]
        public async Task LogException()
        {
            AppSettings.Logger = LogType.Exception;

            ILogger logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(1);
        }
    }
}
