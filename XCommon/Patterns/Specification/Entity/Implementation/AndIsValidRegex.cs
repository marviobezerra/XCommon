using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class AndIsValidRegex<TEntity> : ISpecificationEntity<TEntity>
    {
        private string RegexExpression { get; set; }
        private string PropertyName { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndIsValidRegex(string propertyName, string regexExpression)
            : this(propertyName, regexExpression, "")
        {

        }

        internal AndIsValidRegex(string propertyName, string regexExpression, string message, params object[] args)
        {
            RegexExpression = regexExpression;
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
            var value = (string)property.GetValue(entity);
            Regex regex = new Regex(RegexExpression);
            
            var result = value.IsNotEmpty() && regex.IsMatch(value);

            if (!result && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return result;
        }
    }
}
