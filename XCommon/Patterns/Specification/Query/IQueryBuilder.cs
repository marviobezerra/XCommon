using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.Patterns.Specification.Query
{
	public interface IQueryBuilder<TEntity, TFilter>
	{
		IQueryable<TEntity> Build(IEnumerable<TEntity> query, TFilter filter);
		IQueryable<TEntity> Build(IQueryable<TEntity> query, TFilter filter);
	}
}
