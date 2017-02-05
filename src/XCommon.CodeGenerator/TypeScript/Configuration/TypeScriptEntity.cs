using System;
using System.Collections.Generic;
using System.Reflection;

namespace XCommon.CodeGenerator.TypeScript.Configuration
{
    public class TypeScriptEntity
    {
		public TypeScriptEntity()
		{
			Assemblys = new List<Assembly>();
			TypesExtra = new List<Type>();
            NameOveride = new List<Configuration.NameOveride>();

        }

		public string Path { get; set; }

        public List<NameOveride> NameOveride { get; set; }

        public List<Assembly> Assemblys { get; set; }

		public List<Type> TypesExtra { get; set; }
	}
}
