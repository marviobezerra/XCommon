using System;
using System.Collections.Generic;
using System.Reflection;

namespace XCommon.CodeGeneratorV2.TypeScript.Configuration
{
	public class TypeScriptEntityConfig
    {
		public TypeScriptEntityConfig()
		{
			Assemblys = new List<Assembly>();
			TypesExtra = new List<Type>();
			NameOverride = new List<TypeScriptNameOverride>();
		}

		public bool Execute { get; set; }

		public string Path { get; set; }

		public bool IncludeUtils { get; set; }

		public List<TypeScriptNameOverride> NameOverride { get; set; }

		public List<Assembly> Assemblys { get; set; }

		public List<Type> TypesExtra { get; set; }
	}
}
