using System.Collections.Generic;
using XCommon.CodeGeneratorV2.Angular;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.CSharp.Configuration;
using XCommon.CodeGeneratorV2.TypeScript;
using XCommon.CodeGeneratorV2.TypeScript.Configuration;

namespace XCommon.CodeGeneratorV2
{
	public class GeneratorConfig
    {
		public AngularConfig Angular { get; set; }

		public CSharpConfig CSharp { get; set; }

		public TypeScriptConfig TypeScript { get; set; }

		public DataBaseConfig DataBase { get; set; }

		public IReadOnlyList<DataBaseSchema> DataBaseItems { get; internal set; }
	}
}
