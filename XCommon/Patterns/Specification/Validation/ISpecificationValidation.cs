﻿using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Validation
{
    public interface ISpecificationValidation<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
        bool IsSatisfiedBy(TEntity entity, Execute execute);
    }
}