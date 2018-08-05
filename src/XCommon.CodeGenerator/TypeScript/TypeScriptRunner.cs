using XCommon.CodeGenerator.Core;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.TypeScript
{
	public class TypeScriptRunner : BaseRunner
	{
		[Inject]
		public ITypeScriptEntityWriter TypeScriptEntityWriter { get; set; }

		[Inject]
		public ITypeScriptResourceWriter TypeScriptResourceWriter { get; set; }

		public override int Run()
		{
			if (Config.TypeScript == null)
			{
				return 0;
			}

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
