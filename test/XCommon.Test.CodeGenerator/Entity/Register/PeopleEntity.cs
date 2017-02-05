using System;
using XCommon.Test.CodeGenerator.Entity.Common;

namespace XCommon.Test.CodeGenerator.Entity.Register
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
