using XCommon.Patterns.Repository.Executes;
using System;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class AndIsValid<TEntity> : ISpecificationEntity<TEntity>
    {
        private ISpecificationEntity<TEntity> Spec1 { get; set; }
        private Func<TEntity, bool> Selector { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        public AndIsValid(ISpecificationEntity<TEntity> spec1, Func<TEntity, bool> selector)
            : this(spec1, selector, string.Empty)
        {
        }

        public AndIsValid(ISpecificationEntity<TEntity> spec1, Func<TEntity, bool> selector, string message, params object[] args)
        {
            Spec1 = spec1;
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
            var execute1 = Selector(entity);
            var execute2 = Spec1.IsSatisfiedBy(entity, execute);

            if (!execute1 && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return execute1 && execute2;
        }
    }
}
