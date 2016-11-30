using XCommon.Application.ConsoleX;

namespace XCommon.ProjectGenerator.Application.Commands
{
    public abstract class Command<TParam> : ICommand<TParam>
    {
        protected IConsoleX Console { get; set; } = new ConsoleX();

        public Command(TParam param)
        {
            Parameter = param;
        }

        public TParam Parameter { get; protected set; }

        public virtual void Run()
        {
            Run(Parameter);
        }

        protected abstract void Run(TParam param);
    }
}
