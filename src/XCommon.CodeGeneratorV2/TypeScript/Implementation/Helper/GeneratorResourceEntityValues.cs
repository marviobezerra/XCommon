using System.Collections.Generic;

namespace XCommon.CodeGenerator.TypeScript.Implementation.Helper
{
	internal class GeneratorResourceEntityValues
	{
		public GeneratorResourceEntityValues()
		{
			Properties = new Dictionary<string, string>();
		}

		public string Culture { get; set; }

		public Dictionary<string, string> Properties { get; set; }
	}
}
