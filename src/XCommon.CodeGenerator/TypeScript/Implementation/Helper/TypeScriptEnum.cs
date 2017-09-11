using System;
using System.Collections.Generic;
using System.Text;

namespace XCommon.CodeGenerator.TypeScript.Implementation.Helper
{
	internal class TypeScriptEnum
	{
		public string Name { get; set; }

		public Type Type { get; set; }

		public Dictionary<string, int> Values { get; set; }
	}
}
