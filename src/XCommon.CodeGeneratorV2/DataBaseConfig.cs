using System.Collections.Generic;

namespace XCommon.CodeGenerator
{
	public class DataBaseConfig
    {
		public DataBaseConfig()
		{
			SchemasInclude = new List<string>();
			SchemasExclude = new List<string>();
			TablesExclude = new List<string>();
		}

		public string ConnectionString { get; set; }

		public List<string> SchemasInclude { get; set; }

		public List<string> SchemasExclude { get; set; }

		public List<string> TablesExclude { get; set; }
	}
}
