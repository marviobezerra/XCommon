using System.Collections.Generic;
using XCommon.CodeGerator.Core.Entity;

namespace XCommon.CodeGerator.CSharp.Configuration
{
	public class CSharpConfig
    {
		public string ContractPath { get; set; }

		public string ContractNameSpace { get; set; }

		public string ConcretePath { get; set; }

		public string ConcreteNameSpace { get; set; }

		public string FactoryPath { get; set; }

		public string FacotryNameSpace { get; set; }

		public string EntrityPath { get; set; }

		public string EntrityNameSpace { get; set; }

		public string UnitTestPath { get; set; }

		public string UnitTestNameSpace { get; set; }

		public DataBaseConfig DataBase { get; set; }

		internal List<ItemGroup> DataBaseItems { get; set; }
	}
}
