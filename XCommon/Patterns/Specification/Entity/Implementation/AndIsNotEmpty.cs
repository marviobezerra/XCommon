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

        internal AndIsNotEmpty(Expression<Func<TEntity, TValue>> propertyName, AndIsNotEmptyType type)
            : this(propertyName, type, "")
        {

        }

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

    public class AndIsNotEmptyInt<TEntity> : AndIsNotEmpty<TEntity, int?>
    {
        internal AndIsNotEmptyInt(Expression<Func<TEntity, int?>> propertyName) : base(propertyName, AndIsNotEmptyType.Int)
        {
        }

        internal AndIsNotEmptyInt(Expression<Func<TEntity, int?>> propertyName, string message, params object[] args) : base(propertyName, AndIsNotEmptyType.Int, message, args)
        {
        }
    }

    public class AndIsNotEmptyDecimal<TEntity> : AndIsNotEmpty<TEntity, decimal?>
    {
        internal AndIsNotEmptyDecimal(Expression<Func<TEntity, decimal?>> propertyName) : base(propertyName, AndIsNotEmptyType.Decimal)
        {
        }

        internal AndIsNotEmptyDecimal(Expression<Func<TEntity, decimal?>> propertyName, string message, params object[] args) : base(propertyName, AndIsNotEmptyType.Decimal, message, args)
        {
        }
    }

    public class AndIsNotEmptyDate<TEntity> : AndIsNotEmpty<TEntity, DateTime?>
    {
        internal AndIsNotEmptyDate(Expression<Func<TEntity, DateTime?>> propertyName) : base(propertyName, AndIsNotEmptyType.Date)
        {
        }

        internal AndIsNotEmptyDate(Expression<Func<TEntity, DateTime?>> propertyName, string message, params object[] args) : base(propertyName, AndIsNotEmptyType.Date, message, args)
        {
        }
    }

    public class AndIsNotEmptyString<TEntity> : AndIsNotEmpty<TEntity, string>
    {
        internal AndIsNotEmptyString(Expression<Func<TEntity, string>> propertyName) : base(propertyName, AndIsNotEmptyType.String)
        {
        }

        internal AndIsNotEmptyString(Expression<Func<TEntity, string>> propertyName, string message, params object[] args) : base(propertyName, AndIsNotEmptyType.String, message, args)
        {
        }
    }

    public class AndIsNotEmptyObject<TEntity, TValue> : AndIsNotEmpty<TEntity, TValue>
    {
        internal AndIsNotEmptyObject(Expression<Func<TEntity, TValue>> propertyName) : base(propertyName, AndIsNotEmptyType.Object)
        {
        }

        internal AndIsNotEmptyObject(Expression<Func<TEntity, TValue>> propertyName, string message, params object[] args) : base(propertyName, AndIsNotEmptyType.Object, message, args)
        {
        }
    }
}
