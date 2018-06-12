using System;
using System.Threading.Tasks;
using FluentAssertions;
using XCommon.Application.Logger;
using XCommon.Application.Logger.Implementations;
using XCommon.Application.Settings;
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
		[Trait("Application", "Logger")]
		public async Task LogNone()
        {
            AppSettings.Logger = LogType.None;

            var logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(0);
        }

        [Fact(DisplayName = "Logger (Level Trace)")]
		[Trait("Application", "Logger")]
		public async Task LogTrace()
        {
            AppSettings.Logger = LogType.Trace;

            var logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(5);
        }

        [Fact(DisplayName = "Logger (Level Info)")]
		[Trait("Application", "Logger")]
		public async Task LogInfo()
        {
            AppSettings.Logger = LogType.Info;

            var logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(4);
        }

        [Fact(DisplayName = "Logger (Level Debugger)")]
		[Trait("Application", "Logger")]
		public async Task LogDebugger()
        {
            AppSettings.Logger = LogType.Debug;

            var logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(3);
        }

        [Fact(DisplayName = "Logger (Level Error)")]
		[Trait("Application", "Logger")]
		public async Task LogError()
        {
            AppSettings.Logger = LogType.Error;

            var logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(2);
        }

        [Fact(DisplayName = "Logger (Level Exception)")]
		[Trait("Application", "Logger")]
		public async Task LogException()
        {
            AppSettings.Logger = LogType.Exception;

            var logger = Kernel.Resolve<ILogger>();

            await logger.TraceAsync("Trace level");
            await logger.InfoAsync("Info level");
            await logger.DebugAsync("Debugger level");
            await logger.ErrorAsync("Error leve");
            await logger.ExceptionAsync(new Exception("Teste"), "Exception level");

            logger.LoggerData.Count.Should().Be(1);
        }
    }
}
