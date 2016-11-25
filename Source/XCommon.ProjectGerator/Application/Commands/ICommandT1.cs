namespace XCommon.ProjectGerator.Application.Commands
{
    public interface ICommand<TParam> : ICommand
    {
        TParam Parameter { get; }
    }
}
