using XCommon.CodeGenerator.Core;

namespace XCommon.CodeGenerator.TypeScript.Configuration
{
	public class TypeScriptConfig
    {
		public TypeScriptConfig()
		{
			QuoteType = QuoteType.Double;
		}

		public QuoteType QuoteType { get; set; }

		public TypeScriptEntityConfig Entity { get; set; }

		public TypeScriptResourceConfig Resource { get; set; }
	}
}
