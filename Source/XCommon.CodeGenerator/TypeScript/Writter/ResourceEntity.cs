using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.CodeGenerator.TypeScript
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
