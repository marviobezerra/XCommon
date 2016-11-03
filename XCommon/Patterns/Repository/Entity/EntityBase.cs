using System;
using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Entity
{
    public abstract class EntityBase : EntityPropertyChange
    {
        public EntityAction Action { get; set; }

        [IgnoreDataMember]
        public abstract Guid Key { get; set; }
    }
}
