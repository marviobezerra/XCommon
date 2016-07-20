using System;
using System.Collections.Generic;
using System.Diagnostics;
using XCommon.ProjectGerator.Util;

namespace XCommon.ProjectGerator.Command
{
    public class CommandsList
    {
        private IConsoleX Console { get; set; } = new ConsoleX();

        public CommandsList()
        {
            Commands = new List<ICommand>();
            PostRun = new List<CommandPostRun>();
        }

        public List<ICommand> Commands { get; set; }

        public List<CommandPostRun> PostRun { get; set; }

        public void Run()
        {
            Console.WriteLineBlue("  XCommon Project Generator");
            Console.WriteLine();

            Commands.ForEach(c => c.Run());

            Console.WriteLine();
            
            PostRun.ForEach(pr =>
            {
                Console.WriteLineGreen($"  * {pr.Name}");

                ProcessStartInfo processInfo = new ProcessStartInfo(pr.Command, pr.Arguments)
                {
                    WorkingDirectory = pr.Directory,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden                  
                };

                try
                {
                    var proc = Process.Start(processInfo);
                    proc.WaitForExit();

                    if (proc.ExitCode != 0)
                    {
                        Console.WriteLineYellow($"  ** Warn **: {pr.Name} exists with code diff then 0");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLineRed($"  ** Error **: {pr.Name}: {ex.Message}");
                }
            });

            Console.WriteLine();
            Console.WriteLine("Done!");
        }
    }
}
