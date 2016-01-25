using System;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query.Implementation
{
	internal class QueryBuilderSpecification<TEntity, TFilter>
	{
		public QueryBuilderSpecification(Expression<Func<TEntity, bool>> predicate, Func<TFilter, bool> condition)
		{
			Predicate = predicate;
			Condition = condition ?? (c => true);
		}

		public Expression<Func<TEntity, bool>> Predicate { get; set; }

		public Func<TFilter, bool> Condition { get; set; }
	}
}
