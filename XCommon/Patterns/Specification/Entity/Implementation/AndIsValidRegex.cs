using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class AndIsValidRegex<TEntity> : ISpecificationEntity<TEntity>
    {
        private string RegexExpression { get; set; }
        private Expression<Func<TEntity, string>> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsValidRegex(Expression<Func<TEntity, string>> propertyName, string regexExpression)
            : this(propertyName, regexExpression, "")
        {

        }

        internal AndIsValidRegex(Expression<Func<TEntity, string>> selector, string regexExpression, string message, params object[] args)
        {
            RegexExpression = regexExpression;
            Selector = selector;
            Message = message;
            MessageArgs = args;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, null);
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var property = Selector.Compile();
            var value = property(entity);
            Regex regex = null;

            try
            {
                regex = new Regex(RegexExpression);
            }
            catch (Exception ex)
            {
                if (execute != null)
                    execute.AddMessage(ex, Properties.Resources.InvalidRegex, RegexExpression);

                return false;
            }

            var result = value.IsNotEmpty() && regex.IsMatch(value);

            if (!result && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return result;
        }
    }
}
