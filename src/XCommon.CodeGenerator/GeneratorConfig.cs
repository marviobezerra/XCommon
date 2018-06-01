using System.Collections.Generic;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.TypeScript.Configuration;

namespace XCommon.CodeGenerator
{
	public class GeneratorConfig
    {
		public CSharpConfig CSharp { get; set; }

		public TypeScriptConfig TypeScript { get; set; }

		public DataBaseConfig DataBase { get; set; }

		public IReadOnlyList<DataBaseSchema> DataBaseItems { get; internal set; }
	}
}
