using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using XCommon.Patterns.Specification.Query.Implementation;

namespace XCommon.Patterns.Specification.Query
{
	public class QueryBuilder<TEntity, TFilter> : IQueryBuilder<TEntity, TFilter>
	{
		public QueryBuilder()
		{
			Specifications = new List<QueryBuilderSpecification<TEntity>>();
			Sort = new List<QueryBuilderOrder<TEntity, TFilter>>();
		}

		private int Page { get; set; }

		private int PageSize { get; set; }

		private bool PageApply { get; set; }

		private List<QueryBuilderSpecification<TEntity>> Specifications { get; set; }

		private List<QueryBuilderOrder<TEntity, TFilter>> Sort { get; set; }

		public QueryBuilder<TEntity, TFilter> And(Expression<Func<TEntity, bool>> predicate)
			=> And(predicate, true);

        public QueryBuilder<TEntity, TFilter> And(Expression<Func<TEntity, bool>> predicate, bool apply)
        {
            Specifications.Add(new QueryBuilderSpecification<TEntity>(predicate, apply));
            return this;
        }

        public QueryBuilder<TEntity, TFilter> Or(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, bool>> option)
			=> Or(predicate, option, true);

		public QueryBuilder<TEntity, TFilter> Or(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, bool>> option, bool apply)
		{
			Specifications.Add(new QueryBuilderSpecification<TEntity>(e => predicate.Compile().Invoke(e) || option.Compile().Invoke(e), apply));
			return this;
		}

		private QueryBuilder<TEntity, TFilter> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> property, Func<TFilter, bool> condition, bool descending)
		{
			if (descending)
				Sort.Add(new QueryBuilderOrder<TEntity, TFilter>(items => items.OrderByDescending(property), condition ?? (c => true)));
			else
				Sort.Add(new QueryBuilderOrder<TEntity, TFilter>(items => items.OrderBy(property), condition ?? (c => true)));

			return this;
		}

		public QueryBuilder<TEntity, TFilter> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> property)
			=> OrderBy(property, null, false);

		public QueryBuilder<TEntity, TFilter> OrderBy<TProperty>(Expression<Func<TEntity, TProperty>> property, Func<TFilter, bool> condition)
			=> OrderBy(property, condition, false);

		public QueryBuilder<TEntity, TFilter> OrderByDesc<TProperty>(Expression<Func<TEntity, TProperty>> property)
			=> OrderBy(property, null, true);

		public QueryBuilder<TEntity, TFilter> OrderByDesc<TProperty>(Expression<Func<TEntity, TProperty>> property, Func<TFilter, bool> condition)
			=> OrderBy(property, condition, true);

		public QueryBuilder<TEntity, TFilter> Take(int page, int pageSize)
		{
			if (page > 0 && pageSize > 0)
			{
				Page = page;
				PageSize = pageSize;
				PageApply = true;
			}

			return this;
		}

		public IQueryable<TEntity> Build(IEnumerable<TEntity> query, TFilter filter)
			=> Build(query.AsQueryable(), filter);

		public IQueryable<TEntity> Build(IQueryable<TEntity> query, TFilter filter)
		{
			foreach (var specification in Specifications)
			{
				if (specification.Apply)
					query = query.Where(specification.Predicate);
			}

			foreach (var item in Sort)
			{
				if (item.Condition(filter))
					query = item.Sort(query);
			}

			if (PageApply)
				query = query.Skip((Page - 1) * PageSize).Take(PageSize);

			return query;
		}
	}
}
