using System;
using XCommon.Test.CodeGenerator.Entity.Enum;

namespace XCommon.Test.CodeGenerator.Entity.Common
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
