using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.CodeGenerator.Test.Entity.Register
{
    public class PetEntity
    {
        public string Name { get; set; }

        public string Race { get; set; }

        public PeopleEntity Owers { get; set; }

        public List<PeopleEntity> Friends { get; set; }
    }
}
