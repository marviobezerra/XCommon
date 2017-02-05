using XCommon.Application.Executes;

namespace XCommon.Patterns.Specification.Validation
{
    public interface ISpecificationValidation<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
        bool IsSatisfiedBy(TEntity entity, Execute execute);
    }
}
