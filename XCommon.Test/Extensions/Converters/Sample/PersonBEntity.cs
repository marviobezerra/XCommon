using System;

namespace XCommon.Test.Extensions.Converters.Sample
{
    public class PersonBEntity
    {
        public Guid IdPerson { get; set; }

        public Guid? IdPersonNullable { get; set; }

        public int Age { get; set; }

        public int? AgeNullable { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
