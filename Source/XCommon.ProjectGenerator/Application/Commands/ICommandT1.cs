namespace XCommon.ProjectGenerator.Application.Commands
{
    public interface ICommand<TParam> : ICommand
    {
        TParam Parameter { get; }
    }
}
