using System.Collections.Generic;

namespace XCommon.CodeGenerator.CSharp.Configuration
{
	public class DataBaseConfig
    {
		public DataBaseConfig()
		{
			Remap = new List<DataBaseRemap>();
			Rewrite = new List<DataBaseRewrite>();
			SchemaExclude = new List<string>();
			TableExclude = new List<string>();
		}

		public string ContextName { get; set; }

		public string Path { get; set; }

		public string NameSpace { get; set; }

		public string ConnectionString { get; set; }

		public List<DataBaseRemap> Remap { get; set; }

		public List<DataBaseRewrite> Rewrite { get; set; }

		public List<string> SchemaExclude { get; set; }

		public List<string> TableExclude { get; set; }
	}
}
