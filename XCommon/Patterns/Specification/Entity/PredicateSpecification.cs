using System;
using XCommon.Patterns.Repository.Executes;
using XCommon.Patterns.Specification.Entity.Implementation;

namespace XCommon.Patterns.Specification.Entity
{
    public class PredicateSpecification<TEntity> : SpecificationEntity<TEntity>
    {
        private Predicate<TEntity> Predicate { get; set; }
        private string Message { get; set; }
        private object[] MessageArgs { get; set; }

        public PredicateSpecification(Predicate<TEntity> predicate)
            : this(predicate, string.Empty)
        {
            
        }

        public PredicateSpecification(Predicate<TEntity> predicate, string message, params object[] args)
        {
            Predicate = predicate;
            Message = message;
            MessageArgs = args;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, null);
        }

        public override bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            var result = Predicate(entity);

            if (!result && execute != null)
                execute.AddMessage(ExecuteMessageType.Erro, Message, MessageArgs);

            return result;
        }
    }
}
