using XCommon.CodeGeneratorV2.Core;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2.TypeScript
{
	public class TypeScriptRunner : BaseRunner
	{
		[Inject]
		public ITypeScriptEntityWriter TypeScriptEntityWriter { get; set; }

		[Inject]
		public ITypeScriptResourceWriter TypeScriptResourceWriter { get; set; }

		internal int Run()
		{
			if (Config.TypeScript.Entity != null && Config.TypeScript.Entity.Execute)
			{
				TypeScriptEntityWriter.Run();
			}

			if (Config.TypeScript.Resource != null && Config.TypeScript.Resource.Execute)
			{
				TypeScriptResourceWriter.Run();
			}

			return 0;
		}
	}
}
