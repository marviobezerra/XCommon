using System.Collections.Generic;
using XCommon.CodeGeratorV2.Angular.Configuration;
using XCommon.CodeGeratorV2.Core.Entity;
using XCommon.CodeGeratorV2.CSharp.Configuration;
using XCommon.CodeGeratorV2.TypeScript.Configuration;

namespace XCommon.CodeGeratorV2
{
	public class Configuration
	{
		public AngularConfig Angular { get; set; }

		public CSharpConfig CSharp { get; set; }
		
		public TypeScriptConfig TypeScript { get; set; }

		internal List<ItemGroup> DataBaseItems { get; set; }
	}
}
