using System;
using XCommon.Patterns.Repository.Entity;

namespace XCommon.Test.Entity
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
