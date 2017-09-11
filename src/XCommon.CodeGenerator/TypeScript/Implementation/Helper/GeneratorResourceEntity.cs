using System.Collections.Generic;

namespace XCommon.CodeGenerator.TypeScript.Implementation.Helper
{
	internal class GeneratorResourceEntity
	{
		public GeneratorResourceEntity()
		{
			Values = new List<GeneratorResourceEntityValues>();
		}

		public string ResourceName { get; set; }

		public List<GeneratorResourceEntityValues> Values { get; set; }
	}
}
