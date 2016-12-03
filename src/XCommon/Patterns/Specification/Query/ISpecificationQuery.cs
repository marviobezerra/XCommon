using System.Collections.Generic;
using System.Linq;

namespace XCommon.Patterns.Specification.Query
{
    public interface ISpecificationQuery<TEntity, TFilter>
	{
		IQueryable<TEntity> Build(IEnumerable<TEntity> source, TFilter filter);

		IQueryable<TEntity> Build(IQueryable<TEntity> source, TFilter filter);
	}
}
