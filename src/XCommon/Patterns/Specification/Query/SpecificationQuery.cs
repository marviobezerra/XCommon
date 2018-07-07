using System.Collections.Generic;
using System.Linq;
using XCommon.Patterns.Specification.Query.Implementation;

namespace XCommon.Patterns.Specification.Query
{
    public abstract class SpecificationQuery<TEntity, TFilter> : ISpecificationQuery<TEntity, TFilter>
    {
        public int AppliedFilters { get; private set; }

        public int AppliedOrders { get; private set; }

        public SpecificationList<TEntity, TFilter> NewSpecificationList()
        {
            return new SpecificationList<TEntity, TFilter>();
        }

        protected virtual IQueryable<TEntity> CheckSpecifications(SpecificationList<TEntity, TFilter> specifications, IQueryable<TEntity> source, TFilter filter)
        {
            AppliedFilters = 0;
            AppliedOrders = 0;

			if (source == null || filter == null)
			{
				return new List<TEntity>().AsQueryable();
			}

            foreach (var specification in specifications.Items)
            {
                if (specification.Condition(filter))
                {
                    AppliedFilters++;
                    source = source.Where(specification.Predicate);
                }
            }

            foreach (var item in specifications.Order)
            {
                if (item.Condition(filter))
                {
                    AppliedOrders++;
                    source = item.Sort(source);
                }
            }

            if (specifications.PageNumber > 0 && specifications.PageSize > 0)
			{
				source = source.Skip((specifications.PageNumber - 1) * specifications.PageSize).Take(specifications.PageSize);
			}

			return source;
        }

        public abstract IQueryable<TEntity> Build(IQueryable<TEntity> source, TFilter filter);

        public IQueryable<TEntity> Build(IEnumerable<TEntity> source, TFilter filter)
            => Build(source.AsQueryable(), filter);
    }
}
