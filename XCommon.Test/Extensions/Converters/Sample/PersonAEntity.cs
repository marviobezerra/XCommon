using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.Test.Extensions.Converters.Sample
{
    public class PersonAEntity
    {
        public Guid IdPerson { get; set; }

        public Guid? IdPersonNullable { get; set; }

        public int Age { get; set; }

        public int? AgeNullable { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
