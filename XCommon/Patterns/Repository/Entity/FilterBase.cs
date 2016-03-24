using System;
using System.Collections.Generic;

namespace XCommon.Patterns.Repository.Entity
{
	public abstract class FilterBase
    {
        public FilterBase()
        {
            PageNumber = 1;
            PageSize = 100;

            Keys = new List<Guid>();
        }

        public Guid? Key { get; set; }

        public List<Guid> Keys { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
