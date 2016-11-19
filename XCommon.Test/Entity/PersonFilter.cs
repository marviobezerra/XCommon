using System;
using System.Collections.Generic;

namespace XCommon.Test.Entity
{
    public class PersonFilter
    {
        public PersonFilter()
        {
            Ids = new List<Guid>();
        }

        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
