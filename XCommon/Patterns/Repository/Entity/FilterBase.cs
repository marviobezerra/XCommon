using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Entity
{
    [DataContract]
    public abstract class FilterBase
    {
        public FilterBase()
        {
            PageNumber = 1;
            PageSize = 100;

            Keys = new List<Guid>();
        }

        [DataMember]
        public Guid? Key { get; set; }

        [DataMember]
        public List<Guid> Keys { get; set; }

        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}
