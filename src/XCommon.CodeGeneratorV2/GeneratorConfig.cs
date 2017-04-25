using System;
using System.Collections.Generic;
using System.Text;
using XCommon.CodeGeneratorV2.Angular;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.CSharp.Configuration;
using XCommon.CodeGeneratorV2.TypeScript;

namespace XCommon.CodeGeneratorV2
{
    public class GeneratorConfig
    {
		public AngularConfig Angular { get; set; }

		public CSharpConfig CSharp { get; set; }

		public TypeScriptConfig TypeScript { get; set; }

		internal List<DataBaseSchema> DataBaseItems { get; set; }
	}
}
