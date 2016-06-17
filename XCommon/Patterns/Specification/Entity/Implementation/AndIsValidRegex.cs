using System;
using System.Text.RegularExpressions;
using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    internal class AndIsValidRegex<TEntity> : ISpecificationEntity<TEntity>
    {
        private string RegexExpression { get; set; }
        private Func<TEntity, string> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsValidRegex(Func<TEntity, string> selector, string regexExpression, string message, params object[] args)
        {
            RegexExpression = regexExpression;
            Selector = selector;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
			=> IsSatisfiedBy(entity, null);

		public bool IsSatisfiedBy(TEntity entity, Execute execute)
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
                    execute.AddMessage(ex, "Invalid regex", RegexExpression);

                return false;
            }

            var result = value.IsNotEmpty() && regex.IsMatch(value);

            if (!result && execute != null && !string.IsNullOrEmpty(Message))
                execute.AddMessage(ExecuteMessageType.Error, Message, MessageArgs);

            return result;
        }
    }
}
