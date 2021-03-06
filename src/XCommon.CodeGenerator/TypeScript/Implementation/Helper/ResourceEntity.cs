using System.Collections.Generic;

namespace XCommon.CodeGenerator.TypeScript.Implementation.Helper
{
	internal class ResourceEntity
	{
		internal ResourceEntity()
		{
			Properties = new Dictionary<string, string>();
		}

		internal string ResourceName { get; set; }

		internal Dictionary<string, string> Properties { get; set; }
	}
}
