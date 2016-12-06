using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.Test.CodeGenerator.Entity.Common
{
    public class StatesEntity
    {
        public Guid IdState { get; set; }

        public string Name { get; set; }

        public StatesEntity State { get; set; }

    }
}
