using System;
using System.Collections.Generic;
using System.Text;

namespace XCommon.CodeGeneratorV2.CSharp.Configuration
{
    public class CSharpDataBaseConfig
    {
		public string ContextName { get; set; }

		public string ConnectionString { get; set; }

		public List<CSharpDataBaseRemap> Remap { get; set; }

		public List<CSharpDataBaseRewrite> Rewrite { get; set; }

		public List<string> SchemaExclude { get; set; }

		public List<string> TableExclude { get; set; }
	}
}
