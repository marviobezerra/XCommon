using System;
using System.Linq.Expressions;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    internal class AndIsValid<TEntity> : ISpecificationEntity<TEntity>
    {
        private Expression<Func<TEntity, bool>> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsValid(Expression<Func<TEntity, bool>> selector)
            : this(selector, string.Empty)
        {
        }

        internal AndIsValid(Expression<Func<TEntity, bool>> selector, string message, params object[] args)
        {
            Selector = selector;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, null);
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var func = Selector.Compile();
            var value = func(entity);

            if (!value && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return value;
        }
    }
}
