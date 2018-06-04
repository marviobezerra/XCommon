using System;
using System.Text.RegularExpressions;
using XCommon.Extensions.String;
using XCommon.Application.Executes;
using System.Threading.Tasks;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    internal class AndIsValidRegex<TEntity> : ISpecificationValidation<TEntity>
    {
        private string RegexExpression { get; set; }

        private Func<TEntity, string> Selector { get; set; }

        private string Message { get; set; }

        private object[] MessageArgs { get; set; }

        private Func<TEntity, bool> Condition { get; set; }

        internal AndIsValidRegex(Func<TEntity, string> selector, string regexExpression, bool condition, string message, params object[] args)
            : this(selector, regexExpression, c => condition, message, args)
        {

        }

        internal AndIsValidRegex(Func<TEntity, string> selector, string regexExpression, Func<TEntity, bool> condition, string message, params object[] args)
        {
            RegexExpression = regexExpression;
            Selector = selector;
            Message = message;
            MessageArgs = args;
            Condition = condition;
        }

        public async Task<bool> IsSatisfiedByAsync(TEntity entity)
            => await IsSatisfiedByAsync(entity, null);

        public async Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute)
        {
			return await Task.Factory.StartNew(() => 
			{
				var value = Selector(entity);
				Regex regex = null;

				try
				{
					regex = new Regex(RegexExpression);
				}
				catch (Exception ex)
				{
					if (execute != null)
					{
						execute.AddMessage(ex, "Invalid regex", RegexExpression);
					}

					return false;
				}

				var result = value.IsNotEmpty() && regex.IsMatch(value);

				if (!result && execute != null && !string.IsNullOrEmpty(Message))
				{
					execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);
				}

				return result;
			});
        }
    }
}
