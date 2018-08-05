using System;
using XCommon.Application.CommandLine;

namespace XCommon.CodeGenerator
{
	internal class CustomRunner
    {
		public string TemplateShort { get; set; }

		public string TemplateLong { get; set; }

		public string Description { get; set; }

		public Type RunnerType { get; set; }

		public CommandOption Command { get; set; }
	}
}
