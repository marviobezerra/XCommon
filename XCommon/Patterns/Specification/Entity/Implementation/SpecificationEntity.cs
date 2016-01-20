using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XCommon.Patterns.Repository.Executes;
using XCommon.Util;

namespace XCommon.Patterns.Specification.Entity.Implementation
{
    internal class SpecificationList<TEntity>
    {
        public ISpecificationEntity<TEntity> Specification { get; set; }

        public bool StopIfError { get; set; }
    }

    public class SpecificationEntity<TEntity> : ISpecificationEntity<TEntity>
    {
        public SpecificationEntity()
        {
            Specifications = new List<SpecificationList<TEntity>>();
        }

        private List<SpecificationList<TEntity>> Specifications { get; set; }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return IsSatisfiedBy(entity, new Execute());
        }

        public bool IsSatisfiedBy(TEntity entity, Execute execute)
        {
            bool result = true;

            foreach (var item in this.Specifications)
            {
                var satisfied = item.Specification.IsSatisfiedBy(entity, execute);
                result = result && satisfied;

                if (!result && item.StopIfError)
                    break;
            }

            return result;
        }

        public void Add(ISpecificationEntity<TEntity> specificartion, bool stopIfError = false)
        {
            Specifications.Add(new SpecificationList<TEntity> { Specification = specificartion, StopIfError = stopIfError });
        }        
    }
}
