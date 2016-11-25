namespace XCommon.ProjectGerator.Application.Commands
{
    public class CommandShellParam
    {
        public string Name { get; set; }

        public string Arguments { get; internal set; }

        public string Command { get; internal set; }

        public string Directory { get; internal set; }
    }
}
