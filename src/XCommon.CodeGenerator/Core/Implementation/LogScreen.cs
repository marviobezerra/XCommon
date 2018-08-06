using System;

namespace XCommon.CodeGenerator.Core.Implementation
{
	public class LogScreen : ILog
	{
		public ILog Error(string message)
			=> Print(message, ConsoleColor.Red);

		public ILog Info(string message)
			=> Print(message, ConsoleColor.Green);

		public ILog Warning(string message)
			=> Print(message, ConsoleColor.Yellow);

		public ILog Message(string message)
			=> Print(message, ConsoleColor.Blue);

		public ILog Default(string message)
			=> Print(message, ConsoleColor.White);

		private ILog Print(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
			return this;
		}
	}
}
