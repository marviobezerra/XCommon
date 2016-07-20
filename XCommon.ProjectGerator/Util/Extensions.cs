using System;

namespace XCommon.ProjectGerator.Util
{
    public interface IConsoleX
    {
        IConsoleX WriteLine();

        IConsoleX Clear();

        IConsoleX ClearLine();

        IConsoleX Write(string format, params object[] arg);

        IConsoleX WriteLine(string format, params object[] arg);
    }

    public class ConsoleX : IConsoleX
    {
        public IConsoleX Clear()
        {
            Console.Clear();
            return this;
        }

        public IConsoleX ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
            return this;
        }
        
        public IConsoleX Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
            return this;
        }

        public IConsoleX WriteLine()
        {
            ClearLine();
            WriteLine(string.Empty);
            return this;
        }

        public IConsoleX WriteLine(string format, params object[] arg)
        {
            ClearLine();
            Console.WriteLine(format, arg);
            return this;
        }
    }

    public static class Extensions
    {
        private static IConsoleX Write(this IConsoleX console, ConsoleColor color, string format, params object[] arg)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            console.Write(format, arg);
            Console.ForegroundColor = oldColor;

            return console;
        }

        public static IConsoleX WriteLine(this IConsoleX console, ConsoleColor color, string format, params object[] arg)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            console.WriteLine(format, arg);
            Console.ForegroundColor = oldColor;

            return console;
        }

        public static IConsoleX WriteLineRed(this IConsoleX console, string format, params object[] arg)
        {
            return console.WriteLine(ConsoleColor.Red, format, arg);
        }

        public static IConsoleX WriteRed(this IConsoleX console, string format, params object[] arg)
        {
            return console.Write(ConsoleColor.Red, format, arg);
        }

        public static IConsoleX WriteLineGreen(this IConsoleX console, string format, params object[] arg)
        {
            return console.WriteLine(ConsoleColor.Green, format, arg);
        }

        public static IConsoleX WriteGreen(this IConsoleX console, string format, params object[] arg)
        {
            return console.Write(ConsoleColor.Green, format, arg);
        }

        public static IConsoleX WriteLineBlue(this IConsoleX console, string format, params object[] arg)
        {
            return console.WriteLine(ConsoleColor.Blue, format, arg);
        }

        public static IConsoleX WriteBlue(this IConsoleX console, string format, params object[] arg)
        {
            return console.Write(ConsoleColor.Green, format, arg);
        }

        public static IConsoleX WriteLineWhite(this IConsoleX console, string format, params object[] arg)
        {
            return console.WriteLine(ConsoleColor.White, format, arg);
        }

        public static IConsoleX WriteWhite(this IConsoleX console, string format, params object[] arg)
        {
            return console.Write(ConsoleColor.White, format, arg);
        }

        public static IConsoleX WriteLineYellow(this IConsoleX console, string format, params object[] arg)
        {
            return console.WriteLine(ConsoleColor.Yellow, format, arg);
        }

        public static IConsoleX WriteYellow(this IConsoleX console, string format, params object[] arg)
        {
            return console.Write(ConsoleColor.Yellow, format, arg);
        }
    }
}
