using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XCommon.Patterns.Specification.Query.Implementation
{
    public class SpecificationList<TEntity, TFilter>
    {
        public SpecificationList()
        {
            Items = new List<SpecificationItem<TEntity, TFilter>>();
            Order = new List<SpecificationOrder<TEntity, TFilter>>();
        }

        internal int PageSize { get; set; }

        internal int PageNumber { get; set; }

        internal List<SpecificationItem<TEntity, TFilter>> Items { get; set; }

        internal List<SpecificationOrder<TEntity, TFilter>> Order { get; set; }       
    }
}
