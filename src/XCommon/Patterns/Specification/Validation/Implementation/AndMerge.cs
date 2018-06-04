using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
	public class AndMerge<TEntity> : ISpecificationValidation<TEntity>
	{
		public AndMerge(SpecificationList<TEntity> specificationList, bool condition)
			: this(specificationList, c => condition)
		{
		}

		public AndMerge(SpecificationList<TEntity> specificationList, Func<TEntity, bool> condition)
		{
			Condition = condition;
			SpecificationList = specificationList;
		}

		private SpecificationList<TEntity> SpecificationList { get; set; }

		private Func<TEntity, bool> Condition { get; set; }

		public async Task<bool> IsSatisfiedByAsync(TEntity entity)
			=> await IsSatisfiedByAsync(entity, null);

		public async Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute)
		{
			var result = true;

			if (!Condition(entity))
			{
				return result;
			}

			foreach (var item in SpecificationList.Items)
			{
				result = result && await item.Specification.IsSatisfiedByAsync(entity, execute);
			}

			return result;
		}
	}
}
