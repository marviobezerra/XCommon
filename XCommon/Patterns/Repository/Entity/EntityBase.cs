using System;
using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Entity
{
    [DataContract]
    public abstract class EntityBase : EntityPropertyChange
    {
        [DataMember]
        public EntityAction Action { get; set; }

        [DataMember]
        public abstract Guid Key { get; set; }
    }
}
