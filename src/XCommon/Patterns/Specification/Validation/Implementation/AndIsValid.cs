using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;

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

		public async Task<bool> IsSatisfiedByAsync(TEntity entity)
			=> await IsSatisfiedByAsync(entity, null);

		public async Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute)
		{
			var result = true;
			var condition = await Task.Factory.StartNew(() => Condition(entity));

			if (!condition)
			{
				return result;
			}

			result = await Task.Factory.StartNew(() => Selector(entity));

			if (!result && execute != null)
			{
				execute.AddMessage(ExecuteMessageType.Error, Message ?? "There is a invalid property", MessageArgs ?? new object[] { });
			}

			return result;
		}
	}
}
