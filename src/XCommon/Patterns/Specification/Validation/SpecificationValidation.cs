using System.Threading.Tasks;
using XCommon.Application.Executes;
using XCommon.Patterns.Specification.Validation.Implementation;

namespace XCommon.Patterns.Specification.Validation
{
	public abstract class SpecificationValidation<TEntity> : ISpecificationValidation<TEntity>
	{
		protected SpecificationList<TEntity> NewSpecificationList(bool addCheckNullObject = true)
		{
			var result = new SpecificationList<TEntity>();

			if (addCheckNullObject)
			{
				ISpecificationValidation<TEntity> basicSpecification = new AndIsNotEmpty<TEntity, object>(c => c, AndIsNotEmptyType.Object, true, "Entity {0} can't be null", typeof(TEntity).Name);
				result.Add(basicSpecification, true);
			}

			return result;
		}

		public async Task<bool> IsSatisfiedByAsync(TEntity entity)
		{
			return await IsSatisfiedByAsync(entity, new Execute());
		}

		public abstract Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute);

		protected virtual async Task<bool> CheckSpecificationsAsync(SpecificationList<TEntity> specifications, TEntity entity, Execute execute)
		{
			var result = true;

			foreach (var item in specifications.Items)
			{
				if (!item.Condition(entity))
				{
					continue;
				}

				var satisfied = await item.Specification.IsSatisfiedByAsync(entity, execute);
				result = result && satisfied;

				if (!result && item.StopIfInvalid)
				{
					break;
				}
			}

			return result;
		}
	}
}
