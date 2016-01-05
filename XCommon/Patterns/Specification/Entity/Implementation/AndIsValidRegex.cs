using XCommon.Extensions.String;
using XCommon.Patterns.Repository.Executes;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class AndIsValidRegex<TEntity> : ISpecificationEntity<TEntity>
    {
        private ISpecificationEntity<TEntity> Spec1 { get; set; }
        private string RegexExpression { get; set; }
        private string PropertyName { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        public AndIsValidRegex(ISpecificationEntity<TEntity> spec1, string propertyName, string regexExpression)
            : this(spec1, propertyName, regexExpression, "")
        {

        }

        public AndIsValidRegex(ISpecificationEntity<TEntity> spec1, string propertyName, string regexExpression, string message, params object[] args)
        {
            Spec1 = spec1;
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

            var execute1 = Spec1.IsSatisfiedBy(entity, execute);
            var execute2 = value.IsNotEmpty() && regex.IsMatch(value);

            if (!execute2 && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return execute1 && execute2;
        }
    }
}
