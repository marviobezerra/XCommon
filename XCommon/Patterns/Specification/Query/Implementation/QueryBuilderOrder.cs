using System;
using System.Linq;

namespace XCommon.Patterns.Specification.Query.Implementation
{
	internal class QueryBuilderOrder<TEntity, TFilter>
	{
		public QueryBuilderOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort, Func<TFilter, bool> condition)
		{
			Sort = sort;
			Condition = condition ?? (c => true);
		}

		public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Sort { get; set; }
		
		public Func<TFilter, bool> Condition { get; set; }
	}
}
