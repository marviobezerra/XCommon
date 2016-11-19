using System.Collections.Generic;
using System.Linq;
using XCommon.Patterns.Specification.Query.Implementation;

namespace XCommon.Patterns.Specification.Query
{
    public abstract class SpecificationQuery<TEntity, TFilter> : ISpecificationQuery<TEntity, TFilter>
    {
        public SpecificationQuery()
        {
            Specifications = new SpecificationList<TEntity, TFilter>();
        }

        protected SpecificationList<TEntity, TFilter> Specifications { get; set; }


        protected virtual IQueryable<TEntity> CheckSpecifications(IQueryable<TEntity> source, TFilter filter)
        {
            foreach (var specification in Specifications.Items)
            {
                if (specification.Condition(filter))
                    source = source.Where(specification.Predicate);
            }

            foreach (var item in Specifications.Order)
            {
                if (item.Condition(filter))
                    source = item.Sort(source);
            }

            if (Specifications.PageNumber > 0 && Specifications.PageSize > 0)
                source = source.Skip((Specifications.PageNumber - 1) * Specifications.PageSize).Take(Specifications.PageSize);

            return source;
        }

        public abstract IQueryable<TEntity> Build(IQueryable<TEntity> source, TFilter filter);

        public IQueryable<TEntity> Build(IEnumerable<TEntity> source, TFilter filter)
            => Build(source.AsQueryable(), filter);
    }
}
