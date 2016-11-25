using System;
using XCommon.CodeGerator.Test.Entity.Enum;

namespace XCommon.CodeGerator.Test.Entity.Common
{
    public class CitiesEntity
    {
        public Guid IdCity { get; set; }

        public string Name { get; set; }

        public CityStatus Status { get; set; }

        public QueroQuero? Status2 { get; set; }

        public StatesEntity State { get; set; }
    }
}
