using System.Collections.Generic;
using System.Linq;
using XCommon.ProjectGenerator.Application.Commands;
using XCommon.Application.ConsoleX;
using System;

namespace XCommon.ProjectGenerator.Application
{
    public abstract class ApplicationBase
    {
        protected IConsoleX Console { get; set; } = new ConsoleX();

        public List<ICommand> Commands { get; set; }

        public List<ICommand> CommandsPost { get; set; }

        public ApplicationBase(string[] args)
        {
            Commands = new List<ICommand>();
            CommandsPost = new List<ICommand>();

            SetUp(args);
        }

        protected abstract void SetUp(string[] args);

        protected string[] RemoveArg(string arg, string[] args)
        {
            var result = args.ToList();
            result.Remove(arg);
            return result.ToArray();
        }

        public void Run()
        {
            Commands.ForEach(c => c.Run());
            CommandsPost.ForEach(c => c.Run());
        }

        public static void PrintLogo()
        {
            IConsoleX console = new ConsoleX();

            foreach (var item in Resources.Common.Logo.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                console.WriteLineBlue($"  {item}");
            }
        }
    }
}
