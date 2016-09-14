using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace XCommon.CodeGerator.TypeScript.Configuration
{
    public class TypeScriptEntity
    {
		public TypeScriptEntity()
		{
			Assemblys = new List<Assembly>();
			TypesExtra = new List<Type>();
		}

		public string Path { get; set; }

		public List<Assembly> Assemblys { get; set; }

		public List<Type> TypesExtra { get; set; }
	}
}
