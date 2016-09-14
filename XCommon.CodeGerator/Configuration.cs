using System.Collections.Generic;
using XCommon.CodeGerator.Angular.Configuration;
using XCommon.CodeGerator.Core.Entity;
using XCommon.CodeGerator.CSharp.Configuration;
using XCommon.CodeGerator.TypeScript.Configuration;

namespace XCommon.CodeGerator
{
	public class Configuration
	{
		public AngularConfig Angular { get; set; }

		public CSharpConfig CSharp { get; set; }
		
		public TypeScriptConfig TypeScript { get; set; }

		internal List<ItemGroup> DataBaseItems { get; set; }
	}
}
