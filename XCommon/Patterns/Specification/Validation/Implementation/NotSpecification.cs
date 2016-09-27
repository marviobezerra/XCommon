using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
	internal class NotSpecification<TEntity> : ISpecificationValidation<TEntity>
	{
		private ISpecificationValidation<TEntity> Spec1 { get; set; }
		private ISpecificationValidation<TEntity> Spec2 { get; set; }

		internal NotSpecification(ISpecificationValidation<TEntity> spec1, ISpecificationValidation<TEntity> spec2)
		{
			Spec1 = spec1;
			Spec2 = spec2;
		}

		public bool IsSatisfiedBy(TEntity entity)
			=> IsSatisfiedBy(entity, null);

		public bool IsSatisfiedBy(TEntity entity, Execute execute)
		{
			var execute1 = Spec1.IsSatisfiedBy(entity, execute);
			var execute2 = Spec2.IsSatisfiedBy(entity, execute);

			return execute1 && !execute2;
		}
	}
}
