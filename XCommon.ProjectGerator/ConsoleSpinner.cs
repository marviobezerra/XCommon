using System;
using System.Collections.Generic;
using System.Threading;
using XCommon.Application.ConsoleX;

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
        private IConsoleX Console { get; set; }

        private List<string> Sequence { get; set; }

        private int Counter { get; set; }

        private int Left { get; set; }

        private int Top { get; set; }

        private int Delay { get; set; }

        private bool Active { get; set; }

        private Thread SpinnerThread { get; set; }

        public Spinner(SpinnerSequence sequence = SpinnerSequence.Dots)
            : this(sequence, true, System.Console.CursorLeft)
        {
        }

        public Spinner(SpinnerSequence sequence = SpinnerSequence.Dots, bool autoStart = true)
            : this(sequence, autoStart, System.Console.CursorLeft)
        {
        }

        public Spinner(SpinnerSequence sequence = SpinnerSequence.Dots, bool autoStart = true, int left = 2)
        {
            Console = new ConsoleX();
            SpinnerThread = new Thread(Spin);
            Sequence = new List<string>();

            Delay = 100;
            Counter = 0;
            Top = System.Console.CursorTop;
            Left = left;

            SetSequence(sequence);

            if (autoStart)
                Start();
        }

        public void Start()
        {
            Active = true;
            if (!SpinnerThread.IsAlive)
                SpinnerThread.Start();
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

            var char1 = '.';
            var char2 = ' ';

            result.AddRange(SequenceDotsBuild(size, char1, char2, false));
            result.AddRange(SequenceDotsBuild(size, char2, char1, false));
            result.AddRange(SequenceDotsBuild(size, char1, char2, true));
            result.AddRange(SequenceDotsBuild(size, char2, char1, true));

            return result;
        }

        private List<string> SequenceDotsBuild(int size, char s1, char s2, bool reverse)
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
            Active = false;
            Console.ClearLine();
        }

        private void Spin()
        {
            while (Active)
            {
                Turn();
                Thread.Sleep(Delay);
            }
        }

        private void Draw(string c)
        {
            if (System.Console.CursorTop != Top)
                Top = System.Console.CursorTop;

            System.Console.SetCursorPosition(Left, Top);
            Console.Write(c.ToString());
        }

        private void Turn()
        {
            Draw(Sequence[++Counter % Sequence.Count]);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
