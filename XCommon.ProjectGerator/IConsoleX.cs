using System;
using System.Collections.Generic;
using System.Threading;
using XCommon.ProjectGerator.Util;

namespace XCommon.ProjectGerator
{
    public enum SpinnerSequence
    {
        Dots,
        Slashes,
        Circles,
        Crosses,
        Arrows
    }

    public class Spinner : IDisposable
    {
        protected IConsoleX Console { get; set; } = new ConsoleX();

        /*
                     delay = iDelay;
            if (sSequence == "dots")
            {
                sequence = new string[] { ".   ", "..  ", "... ", "...." };
                loop = true;
            }
            else if (sSequence == "slashes")
                sequence = new string[] { "/", "-", "\\", "|" };
            else if (sSequence == "circles")
                sequence = new string[] ;
            else if (sSequence == "crosses")
                sequence = new string[] { "+", "x" };
            else if (sSequence == "arrows")
                sequence = new string[] { "V", "<", "^", ">" };
             
             */

        private List<string> Sequence = new List<string>();
        private int counter = 0;
        private int left = System.Console.CursorLeft;
        private int top = System.Console.CursorTop;
        private int delay = 100;
        private bool active;
        private readonly Thread thread;

        public Spinner(SpinnerSequence sequence = SpinnerSequence.Crosses)
        {
            SetSequence(sequence);
            thread = new Thread(Spin);
        }

        public void Start()
        {
            active = true;
            if (!thread.IsAlive)
                thread.Start();
        }

        private void SetSequence(SpinnerSequence sequence)
        {
            switch (sequence)
            {
                case SpinnerSequence.Dots:
                    Sequence = GetSequenceDots(9);
                        break;
                
                case SpinnerSequence.Circles:
                    Sequence = new List<string> { ".", "o", "0", "o" };
                    break;
                case SpinnerSequence.Crosses:
                    Sequence = new List<string> { "+", "x", "+", "x" };
                    break;
                case SpinnerSequence.Arrows:
                    Sequence = new List<string> { "V", "<", "^", ">" };
                    break;
                case SpinnerSequence.Slashes:
                default:
                    Sequence = new List<string> { "/", "-", "\\", "|" };
                    break;
            }
        }

        public List<string> GetSequenceDots(int size)
        {
            List<string> result = new List<string>();

            var s1 = '.';
            var s2 = ' ';

            result.AddRange(SequenceDtosBuild(size, s1, s2, false));
            result.AddRange(SequenceDtosBuild(size, s2, s1, false));
            result.AddRange(SequenceDtosBuild(size, s1, s2, true));
            result.AddRange(SequenceDtosBuild(size, s2, s1, true));


            return result;
        }

        private List<string> SequenceDtosBuild(int size, char s1, char s2, bool reverse)
        {
            List<string> result = new List<string>();

            string line = string.Empty;

            for (int i = 1; i < size + 1; i++)
            {
                line = string.Empty.PadRight(i, s1);

                if (reverse)
                    line = line.PadLeft(size, s2);
                else
                    line = line.PadRight(size, s2);

                result.Add(line);
            }

            return result;
        }

        public void Stop()
        {
            active = false;
            Draw(" ");
        }

        private void Spin()
        {
            while (active)
            {
                Turn();
                Thread.Sleep(delay);
            }
        }

        private void Draw(string c)
        {
            if (System.Console.CursorTop != top)
                top = System.Console.CursorTop;

            System.Console.SetCursorPosition(left, top);
            Console.Write(c.ToString());
        }

        private void Turn()
        {
            Draw(Sequence[++counter % Sequence.Count]);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
