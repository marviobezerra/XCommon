using System;
using XCommon.CodeGerator.Test.Entity.Common;

namespace XCommon.CodeGerator.Test.Entity.Register
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
