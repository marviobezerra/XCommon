using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class OrSpecification<TEntity> : ISpecificationEntity<TEntity>
    {
        private ISpecificationEntity<TEntity> Spec1 { get; set; }
        private ISpecificationEntity<TEntity> Spec2 { get; set; }

        internal OrSpecification(ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
        {
            Spec1 = spec1;
            Spec2 = spec2;
        }
        
        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, null);
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            Execute executeInternal1 = new Execute();
            Execute executeInternal2 = new Execute();
            
            var result1 = Spec1.IsSatisfiedBy(entity, executeInternal1);
            var result2 = Spec2.IsSatisfiedBy(entity, executeInternal2);
            var result = result1 || result2;

            if (!result && execute != null)
            {
                execute.AddMessage(executeInternal1);
                execute.AddMessage(executeInternal2);
            }

            return result;
        }
    }
}
