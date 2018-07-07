using System;
using System.Collections.Generic;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Test.Entity
{
    public class PersonFilter : FilterBase
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
    }
}
