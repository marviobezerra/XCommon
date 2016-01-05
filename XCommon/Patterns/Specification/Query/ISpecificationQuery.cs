using System;
using System.Linq;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query
{
    public interface ISpecificationQuery<TEntity, TFilter>
    {
        Expression<Func<TFilter, bool>> CanApllyBy();
        Expression<Func<TEntity, bool>> IsSatisfied();
    }
}
