using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.CSharp.Configuration
{
	public class CSharpDataBaseConfig
    {
		public CSharpDataBaseConfig()
		{
			Schemas = new List<string>();
			SchemaExclude = new List<string>();
			TablesExclude = new List<string>();
		}

		public string ContextName { get; set; }

		public string ConnectionString { get; set; }

		public List<CSharpDataBaseRemap> Remap { get; set; }

		public List<CSharpDataBaseRewrite> Rewrite { get; set; }

		public List<string> Schemas { get; set; }

		public List<string> SchemaExclude { get; set; }

		public List<string> TablesExclude { get; set; }
	}
}
