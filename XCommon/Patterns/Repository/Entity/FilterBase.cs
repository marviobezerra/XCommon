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

            Ids = new List<Guid>();
        }

        [DataMember]
        public Guid? Id { get; set; }

        [DataMember]
        public List<Guid> Ids { get; set; }

        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}
