using System;
using XCommon.CodeGenerator.Test.Entity.Common;

namespace XCommon.CodeGenerator.Test.Entity.Register
{
    public class PeopleEntity
    {
        public Guid IdPerson { get; set; }

        public string Name { get; set; }

        public CitiesEntity City { get; set; }

        public StatesEntity State { get; set; }

        public Enum.CityStatus Status { get; set; }
    }
}
