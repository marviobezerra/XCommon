using System;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query.Implementation
{
	internal class QueryBuilderSpecification<TEntity, TFilter>
	{
		public QueryBuilderSpecification(Expression<Func<TEntity, TFilter, bool>> predicate, Func<TFilter, bool> condition)
		{
			Predicate = predicate;
			Condition = condition ?? (c => true);
		}

		public Expression<Func<TEntity, TFilter, bool>> Predicate { get; set; }

        public Expression<Func<TEntity, bool>> PredicateX(TFilter filter)
        {
            return entity => Predicate.Compile().Invoke(entity, filter);
        }

        public Func<TFilter, bool> Condition { get; set; }
	}
}
