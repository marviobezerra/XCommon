using System;
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

    public class AndIsNotEmpty<TEntity> : ISpecificationEntity<TEntity>
    {
        private AndIsNotEmptyType Type { get; set; }
        private string PropertyName { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        public AndIsNotEmpty(string propertyName, AndIsNotEmptyType type)
            : this(propertyName, type, "")
        {

        }

        public AndIsNotEmpty(string propertyName, AndIsNotEmptyType type, string message, params object[] args)
        {
            Type = type;
            PropertyName = propertyName;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, null);
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var property = typeof(TEntity).GetProperty(PropertyName);
            var value = property.GetValue(entity);

            bool result = true;

            switch (Type)
            {
                case AndIsNotEmptyType.String:
                    result = value.ToString().IsNotEmpty();
                    break;
                case AndIsNotEmptyType.Int:
                    result = ((int?)value).HasValue;
                    break;
                case AndIsNotEmptyType.Decimal:
                    result = ((decimal?)value).HasValue;
                    break;
                case AndIsNotEmptyType.Date:
                    result = ((DateTime?)value).HasValue;
                    break;
                case AndIsNotEmptyType.Object:
                default:
                    result = value != null;
                    break;
            }

            if (!result && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return result;
        }
    }
}
