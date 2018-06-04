using System;
using System.Threading.Tasks;
using XCommon.Application.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
	internal class Or<TEntity> : ISpecificationValidation<TEntity>
	{
		private ISpecificationValidation<TEntity> Spec1 { get; set; }

		private ISpecificationValidation<TEntity> Spec2 { get; set; }

		private Func<TEntity, bool> Condition { get; set; }

		internal Or(ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, bool condition)
			: this(spec1, spec2, c => condition)
		{
		}

		internal Or(ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2, Func<TEntity, bool> condition)
		{
			Spec1 = spec1;
			Spec2 = spec2;
			Condition = condition;
		}

		public async Task<bool> IsSatisfiedByAsync(TEntity entity)
			=> await IsSatisfiedByAsync(entity, null);

		public async Task<bool> IsSatisfiedByAsync(TEntity entity, Execute execute)
		{
			var result = true;

			if (!Condition(entity))
			{
				return result;
			}

			var executeInternal1 = new Execute();
			var executeInternal2 = new Execute();

			var result1 = await Spec1.IsSatisfiedByAsync(entity, executeInternal1);
			var result2 = await Spec2.IsSatisfiedByAsync(entity, executeInternal2);
			result = result1 || result2;

			if (!result && execute != null)
			{
				execute.AddMessage(executeInternal1);
				execute.AddMessage(executeInternal2);
			}

			return result;
		}
	}
}
