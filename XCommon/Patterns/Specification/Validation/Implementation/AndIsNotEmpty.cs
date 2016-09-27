using System;
using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    public enum AndIsNotEmptyType
    {
        String,
        Int,
        Decimal,
        Date,
        Object
    }

    internal class AndIsNotEmpty<TEntity, TValue> : ISpecificationValidation<TEntity>
    {
        private AndIsNotEmptyType Type { get; set; }
        private Func<TEntity, TValue> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsNotEmpty(Func<TEntity, TValue> selector, AndIsNotEmptyType type, string message, params object[] args)
        {
            Type = type;
            Selector = selector;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
			=> IsSatisfiedBy(entity, new Execute());

		public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var value = Selector(entity);

            bool result = true;

            switch (Type)
            {
                case AndIsNotEmptyType.String:
                    result = (value as string).IsNotEmpty();
                    break;
                case AndIsNotEmptyType.Int:
                    result = (value as int?).HasValue;
                    break;
                case AndIsNotEmptyType.Decimal:
                    result = (value as decimal?).HasValue;
                    break;
                case AndIsNotEmptyType.Date:
                    result = (value as DateTime?).HasValue;
                    break;
                case AndIsNotEmptyType.Object:
                default:
                    result = value != null;
                    break;
            }

            if (!result && execute != null && Message.IsNotEmpty())
                execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);

            return result;
        }
    }
}
