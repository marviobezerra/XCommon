using System;
using System.Runtime.Serialization;
using XCommon.Util;

namespace XCommon.Patterns.Repository.Entity
{
    public abstract class EntityBase : EntityPropertyChange
    {
        public EntityAction Action { get; set; }

        [Ignore]
        public abstract Guid Key { get; set; }
    }
}
