using System;
using System.Diagnostics;
using XCommon.Application.ConsoleX;

namespace XCommon.ProjectGerator.Application.Commands
{
    public class CommandShell : Command<CommandShellParam>
    {
        public CommandShell(CommandShellParam param) 
            : base(param)
        {
        }

        protected override void Run(CommandShellParam param)
        {
            Console.WriteLineGreen($"  * {param.Name}");

            ProcessStartInfo processInfo = new ProcessStartInfo(param.Command, param.Arguments)
            {
                WorkingDirectory = param.Directory,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                var proc = Process.Start(processInfo);
                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    Console.WriteLineYellow($"    ## WARN ##: {param.Name} exists with code diff then 0");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLineRed($"    ## ERROR ##: {param.Name}: {ex.Message}");
            }
        }
    }
}
