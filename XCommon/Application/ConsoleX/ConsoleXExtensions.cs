using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.Application.ConsoleX
{
    public static class ConsoleXExtensions
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
