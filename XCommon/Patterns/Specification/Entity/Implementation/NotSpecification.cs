using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public class NotSpecification<TEntity> : ISpecificationEntity<TEntity>
    {
        private ISpecificationEntity<TEntity> Spec1 { get; set; }
        private ISpecificationEntity<TEntity> Spec2 { get; set; }

        public NotSpecification(ISpecificationEntity<TEntity> spec1, ISpecificationEntity<TEntity> spec2)
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
            var execute1 = Spec1.IsSatisfiedBy(entity, execute);
            var execute2 = Spec2.IsSatisfiedBy(entity, execute);

            return execute1 && !execute2;
        }
    }
}
