using System;
using XCommon.Extensions.String;
using XCommon.Application.Executes;
using System.Threading.Tasks;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    internal enum AndIsNotEmptyType
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

        private Func<TEntity, bool> Condition { get; set; }

        internal AndIsNotEmpty(Func<TEntity, TValue> selector, AndIsNotEmptyType type, bool condition, string message, params object[] args)
            : this(selector, type, c => condition, message, args)
        {
        }

        internal AndIsNotEmpty(Func<TEntity, TValue> selector, AndIsNotEmptyType type, Func<TEntity, bool> condition, string message, params object[] args)
        {
            Type = type;
            Selector = selector;
            Message = message;
            MessageArgs = args;
            Condition = condition;
        }

        public async Task<bool> IsSatisfiedByAsync(TEntity entity)
            => await IsSatisfiedByAsync(entity, new Execute());

        public async Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute)
        {
			return await Task.Factory.StartNew(() => 
			{
				var result = true;

				if (!Condition(entity))
				{
					return result;
				}

				var value = Selector(entity);

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

				if (!result && execute != null)
				{
					execute.AddMessage(ExecuteMessageType.Error, Message ?? "There is a empty property", MessageArgs ?? new object[] { });
				}

				return result;
			});
        }
    }
}
