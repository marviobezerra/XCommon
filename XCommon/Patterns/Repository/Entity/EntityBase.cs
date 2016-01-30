using System;
using System.Runtime.Serialization;
using XCommon.Util;

namespace XCommon.Patterns.Repository.Entity
{
    [DataContract]
    public abstract class EntityBase : EntityPropertyChange
    {
        [DataMember]
        public EntityAction Action { get; set; }

        [DataMember, Ignore]
        public abstract Guid Key { get; set; }
    }
}
