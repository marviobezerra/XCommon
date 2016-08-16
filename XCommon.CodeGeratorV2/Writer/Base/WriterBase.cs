using System.Collections.Generic;
using XCommon.Application.ConsoleX;

namespace XCommon.CodeGeratorV2.Writer.Base
{
    public abstract class WriterBase
    {
        public WriterBase()
        {
            Console = new ConsoleX();
        }

        protected static List<string> Warnings { get; private set; } = Generator.Warnings;

        protected static List<string> Error { get; private set; } = Generator.Error;

        public ConsoleX Console { get; private set; }

        public void CheckDirectory(string name)
        {
            if (!System.IO.Directory.Exists(name))
                System.IO.Directory.CreateDirectory(name);
        }

        public void WriteFile(string name, string content)
        {
            CheckDirectory(System.IO.Path.GetDirectoryName(name));

            System.IO.File.WriteAllText(name, content, System.Text.Encoding.UTF8);
        }
    }
}
