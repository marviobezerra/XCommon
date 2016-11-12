using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Test.Patterns.Specification.Validation.Sample
{
    public class PersonEntity
    {
        public EntityAction Action { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }
    }
}
