namespace XCommon.ProjectGeratorV2.Application.Commands
{
    public interface ICommand<TParam> : ICommand
    {
        TParam Parameter { get; }
    }
}
