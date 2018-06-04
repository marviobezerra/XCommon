using System.Threading.Tasks;
using XCommon.Application.Executes;

namespace XCommon.Patterns.Specification.Validation
{
    public interface ISpecificationValidation<TEntity>
    {
        Task<bool> IsSatisfiedByAsync(TEntity entity);
        Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute);
    }
}
