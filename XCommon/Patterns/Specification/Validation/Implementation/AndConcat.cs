﻿using System;
using XCommon.Patterns.Repository.Executes;

namespace XCommon.Patterns.Specification.Validation.Implementation
{
    public class AndConcat<TEntity> : ISpecificationValidation<TEntity>
    {
        public AndConcat(SpecificationList<TEntity> specificationList, bool condition)
            : this(specificationList, c => condition)
        {
        }

        public AndConcat(SpecificationList<TEntity> specificationList, Func<TEntity, bool> condition)
        {
            Condition = condition;
            SpecificationList = specificationList;
        }

        private SpecificationList<TEntity> SpecificationList { get; set; }

        private Func<TEntity, bool> Condition { get; set; }

        public bool IsSatisfiedBy(TEntity entity)
            => IsSatisfiedBy(entity, null);

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            bool result = true;

            if (!Condition(entity))
                return result;

            SpecificationList.Items.ForEach(specification => 
            {
                result = result && specification.Specification.IsSatisfiedBy(entity, execute);
            });
            
            return result;
        }
    }
}
