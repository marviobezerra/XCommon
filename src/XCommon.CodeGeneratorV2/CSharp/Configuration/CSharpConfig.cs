using System;
using System.Collections.Generic;
using System.Text;

namespace XCommon.CodeGeneratorV2.CSharp.Configuration
{
    public class CSharpConfig
    {
		public CSharpProjectConfig Contract { get; set; }

		public CSharpProjectConfig Concrete { get; set; }

		public CSharpProjectConfig Factory { get; set; }

		public CSharpProjectConfig Entrity { get; set; }

		public CSharpProjectConfig UnitTest { get; set; }

		public CSharpDataBaseConfig DataBase { get; set; }
	}
}
