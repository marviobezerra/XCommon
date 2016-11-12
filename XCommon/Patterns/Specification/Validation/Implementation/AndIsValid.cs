using System;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    internal class AndIsValid<TEntity> : ISpecificationValidation<TEntity>
    {
        private Func<TEntity, bool> Selector { get; set; }

        private string Message { get; set; }

        private object[] MessageArgs { get; set; }

        private Func<TEntity, bool> Condition { get; set; }

        internal AndIsValid(Func<TEntity, bool> selector, bool condition, string message, params object[] args)
            : this(selector, c => condition, message, args)
        {
        }

        internal AndIsValid(Func<TEntity, bool> selector, Func<TEntity, bool> condition, string message, params object[] args)
        {
            Selector = selector;
            Message = message;
            MessageArgs = args;
            Condition = condition;
        }

        public bool IsSatisfiedBy(TEntity entity)
            => IsSatisfiedBy(entity, null);

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            bool result = true;

            if (!Condition(entity))
                return result;

            result = Selector(entity);

            if (!result && execute != null)
                execute.AddMessage(ExecuteMessageType.Error, Message ?? "There is a invalid property", MessageArgs ?? new object[] { });

            return result;
        }
    }
}
