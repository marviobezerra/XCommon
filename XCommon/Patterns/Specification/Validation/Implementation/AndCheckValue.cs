using System;
using XCommon.Extensions.Checks;
using XCommon.Extensions.Converters;
using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    public enum AndCheckValueType
    {
        Int,
        Decimal,
        Date
    }

    public enum AndCheckCompareType
    {
        BiggerThan,
        LessThan,
        InRange
    }

    internal class AndCheckValue<TEntity, TValue> : ISpecificationValidation<TEntity>
    {
        private AndCheckValueType Type { get; set; }
        private AndCheckCompareType CompareType { get; set; }
        private Func<TEntity, TValue> PropertyValue { get; set; }
        private Func<TEntity, TValue> PropertyStart { get; set; }
        private Func<TEntity, TValue> PropertyEnd { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }
        private bool RemoveTime { get; set; }
        
        internal AndCheckValue(Func<TEntity, TValue> value, Func<TEntity, TValue> start, Func<TEntity, TValue> end, AndCheckValueType type, AndCheckCompareType compareType, string message, params object[] args)
            : this(value, start, end, type, compareType, false, message, args)
        {
            
        }

        internal AndCheckValue(Func<TEntity, TValue> value, Func<TEntity, TValue> start, Func<TEntity, TValue> end, AndCheckValueType type, AndCheckCompareType compareType, bool removeTime, string message, params object[] args)
        {
            Type = type;
            CompareType = compareType;
            PropertyValue = value;
            PropertyStart = start;
            PropertyEnd = end;
            Message = message;
            MessageArgs = args;
            RemoveTime = removeTime;
        }

        public bool IsSatisfiedBy(TEntity entity)
			=> IsSatisfiedBy(entity, new Execute());

		public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            switch (Type)
            {
                case AndCheckValueType.Int:
                    return IsSatisfiedByInt(entity, execute);
                case AndCheckValueType.Decimal:
                    return IsSatisfiedByDecimal(entity, execute);
                case AndCheckValueType.Date:
                default:
                    return IsSatisfiedByDate(entity, execute);
            }
        }

        private TValue Resolve(Func<TEntity, TValue> expression, TEntity entity)
        {
            var result = default(TValue);

            if (expression == null)
                return result;
            
            var value = expression(entity);

            return value;
        }

        private bool IsSatisfiedByInt(TEntity entity, Execute execute)
        {
            int value = Resolve(PropertyValue, entity).ToInt32();
            int start = Resolve(PropertyStart, entity).ToInt32();
            int end = Resolve(PropertyEnd, entity).ToInt32();

            bool result = true;

            switch (CompareType)
            {
                case AndCheckCompareType.BiggerThan:
                    result = value.BiggerThan(start);
                    break;
                case AndCheckCompareType.LessThan:
                    result = value.LessThan(start);
                    break;
                case AndCheckCompareType.InRange:
                default:
                    result = value.InRange(start, end);

                    break;
            }

            if (!result && execute != null && Message.IsNotEmpty())
                execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);

            return result;
        }

        private bool IsSatisfiedByDecimal(TEntity entity, Execute execute)
        {
            decimal value = Resolve(PropertyValue, entity).ToDecimal();
            decimal start = Resolve(PropertyStart, entity).ToDecimal();
            decimal end = Resolve(PropertyEnd, entity).ToDecimal();

            bool result = true;

            switch (CompareType)
            {
                case AndCheckCompareType.BiggerThan:
                    result = value.BiggerThan(start);
                    break;
                case AndCheckCompareType.LessThan:
                    result = value.LessThan(start);
                    break;
                case AndCheckCompareType.InRange:
                default:
                    result = value.InRange(start, end);

                    break;
            }

            if (!result && execute != null && Message.IsNotEmpty())
                execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);

            return result;
        }

        private bool IsSatisfiedByDate(TEntity entity, Execute execute)
        {
            DateTime value = Resolve(PropertyValue, entity).ToDateTime();
            DateTime start = Resolve(PropertyStart, entity).ToDateTime();
            DateTime end = Resolve(PropertyEnd, entity).ToDateTime();

            bool result = true;

            switch (CompareType)
            {
                case AndCheckCompareType.BiggerThan:
                    result = value.BiggerThan(start, RemoveTime);
                    break;
                case AndCheckCompareType.LessThan:
                    result = value.LessThan(start, RemoveTime);
                    break;
                case AndCheckCompareType.InRange:
                default:
                    result = value.InRange(start, end, RemoveTime);

                    break;
            }

            if (!result && execute != null && Message.IsNotEmpty())
                execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);

            return result;
        }
    }
}
