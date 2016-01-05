
namespace XCommon.Patterns.Specification.Entity.Implementation
{
    public abstract class SpecificationEntity<TEntity> : ISpecificationEntity<TEntity>
    {
        public static SpecificationEntity<TEntity> operator &(SpecificationEntity<TEntity> left, SpecificationEntity<TEntity> right)
        {
            return new PredicateSpecification<TEntity>(t => left.IsSatisfiedBy(t) && right.IsSatisfiedBy(t));
        }

        public static SpecificationEntity<TEntity> operator |(SpecificationEntity<TEntity> left, SpecificationEntity<TEntity> right)
        {
            return new PredicateSpecification<TEntity>(t => left.IsSatisfiedBy(t) || right.IsSatisfiedBy(t));
        }

        public static SpecificationEntity<TEntity> operator !(SpecificationEntity<TEntity> specification)
        {
            return new PredicateSpecification<TEntity>(t => !specification.IsSatisfiedBy(t));
        }

        public static bool operator true(SpecificationEntity<TEntity> specification)
        {
            return false;
        }

        public static bool operator false(SpecificationEntity<TEntity> specification)
        {
            return false;
        }

        public abstract bool IsSatisfiedBy(TEntity item);

        public abstract bool IsSatisfiedBy(TEntity entity, Repository.Executes.Execute execute);
    }
}
