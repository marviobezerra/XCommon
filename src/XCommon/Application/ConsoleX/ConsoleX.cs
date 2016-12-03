using System;

namespace XCommon.Application.ConsoleX
{
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

    
}
