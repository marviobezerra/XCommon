using XCommon.Patterns.Repository.Executes;
using System;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class AndIsValid<TEntity> : ISpecificationEntity<TEntity>
    {
        private Func<TEntity, bool> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        public AndIsValid(Func<TEntity, bool> selector)
            : this(selector, string.Empty)
        {
        }

        public AndIsValid(Func<TEntity, bool> selector, string message, params object[] args)
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
            var result = Selector(entity);

            if (!result && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return result;
        }
    }
}
