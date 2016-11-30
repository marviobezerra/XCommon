using System.Collections.Generic;
using XCommon.CodeGenerator.Angular.Configuration;
using XCommon.CodeGenerator.Core.Entity;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.TypeScript.Configuration;

namespace XCommon.CodeGenerator
{
	public class Configuration
	{
		public AngularConfig Angular { get; set; }

		public CSharpConfig CSharp { get; set; }
		
		public TypeScriptConfig TypeScript { get; set; }

		internal List<ItemGroup> DataBaseItems { get; set; }
	}
}
