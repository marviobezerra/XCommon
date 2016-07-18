﻿using System;

namespace XCommon.ProjectGerator.Command
{
    public interface ICommand
    {
        void Run();
    }

    public interface ICommand<TParam> : ICommand
    {
        TParam Parameter { get; }
    }

    public abstract class Command<TParam> : ICommand<TParam>
    {
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
