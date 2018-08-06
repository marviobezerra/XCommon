namespace XCommon.CodeGenerator.Core
{
	public interface ILog
    {
		ILog Default(string message);
		ILog Info(string message);
		ILog Message(string message);
		ILog Warning(string message);
		ILog Error(string message);

	}
}
