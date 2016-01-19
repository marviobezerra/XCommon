using System;
using System.Linq.Expressions;
using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public enum AndIsNotEmptyType
    {
        String,
        Int,
        Decimal,
        Date,
        Object
    }

    public class AndIsNotEmpty<TEntity, TValue> : ISpecificationEntity<TEntity>
    {
        private AndIsNotEmptyType Type { get; set; }
        private Expression<Func<TEntity, TValue>> PropertyName { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsNotEmpty(Expression<Func<TEntity, TValue>> propertyName, AndIsNotEmptyType type, string message, params object[] args)
        {
            Type = type;
            PropertyName = propertyName;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, new Execute());
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var func = PropertyName.Compile();
            var value = func(entity);

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
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return result;
        }
    }
}
