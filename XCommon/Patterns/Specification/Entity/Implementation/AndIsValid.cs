using System;
using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    internal class AndIsValid<TEntity> : ISpecificationEntity<TEntity>
    {
        private Func<TEntity, bool> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsValid(Func<TEntity, bool> selector, string message, params object[] args)
        {
            Selector = selector;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
			=> IsSatisfiedBy(entity, null);

		public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var value = Selector(entity);

            if (!value && execute != null && Message.IsNotEmpty())
                execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);

            return value;
        }
    }
}
