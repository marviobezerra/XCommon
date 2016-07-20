namespace XCommon.Application.ConsoleX
{
    public interface IConsoleX
    {
        IConsoleX WriteLine();

        IConsoleX Clear();

        IConsoleX ClearLine();

        IConsoleX Write(string format, params object[] arg);

        IConsoleX WriteLine(string format, params object[] arg);
    }
}
