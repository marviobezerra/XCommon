using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public enum AndCheckValueType
    {
        Int,
        Decimal,
        Date
    }

    public class AndCheckValue<TEntity> : ISpecificationEntity<TEntity>
    {
        private AndCheckValueType Type { get; set; }
        private string PropertyName { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        internal AndCheckValue(string propertyName, AndCheckValueType type)
            : this(propertyName, type, "")
        {

        }

        internal AndCheckValue(string propertyName, AndCheckValueType type, string message, params object[] args)
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

            if (value is int)
            {


            }

            return result;
        }
    }
}
