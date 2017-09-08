using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.CSharp.Configuration
{
	public class CSharpEntityFrameworkConfig : CSharpProjectConfig
	{
		public string ContextName { get; set; }

		public List<CSharpDataBaseRemap> Remap { get; set; }

		public List<CSharpDataBaseRewrite> Rewrite { get; set; }
	}
}
