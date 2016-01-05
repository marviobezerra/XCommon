using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Entity
{
    public interface ISpecificationEntity<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
        bool IsSatisfiedBy(TEntity entity, Execute execute);
    }
}
