using System;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query.Implementation
{
	internal class QueryBuilderSpecification<TEntity>
	{
		public QueryBuilderSpecification(Expression<Func<TEntity, bool>> predicate, bool apply)
		{
			Predicate = predicate;
            Apply = apply;
		}

		public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public bool Apply { get; set; }
	}
}
