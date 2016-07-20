using System;
using System.Collections.Generic;
using System.Diagnostics;
using XCommon.Application.ConsoleX;

namespace XCommon.ProjectGerator.Command
{
    public class CommandsList
    {
        private IConsoleX Console { get; set; } = new ConsoleX();

        public CommandsList()
        {
            PrintLogo();
            Commands = new List<ICommand>();
            PostRun = new List<CommandPostRun>();
        }
        
        public List<ICommand> Commands { get; set; }

        public List<CommandPostRun> PostRun { get; set; }

        private void PrintLogo()
        {
            foreach (var item in Resources.CSharp.Logo.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                Console.WriteLineBlue($"  {item}");
            }
        }

        public void Run()
        {
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
                        Console.WriteLineYellow($"    ## WARN ##: {pr.Name} exists with code diff then 0");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLineRed($"    ## ERROR ##: {pr.Name}: {ex.Message}");
                }
            });

            Console.WriteLine();
            Console.WriteLine("Done!");
        }
    }
}
