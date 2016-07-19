using System.Collections.Generic;

namespace XCommon.ProjectGerator.Command
{
    public class CommandsList
    {
        public CommandsList()
        {
            Commands = new List<ICommand>();
        }

        public List<ICommand> Commands { get; set; }

        public void Run()
        {
            Commands.ForEach(c => c.Run());
        }
    }
}
