using XCommon.CodeGenerator.Angular.Writter;
using XCommon.CodeGenerator.TypeScript.Configuration;
using XCommon.CodeGenerator.TypeScript.Writter;

namespace XCommon.CodeGenerator.TypeScript
{
	internal class TypeScriptRunner
    {
		internal int Run(TypeScriptConfig config)
		{
			Entities entity = new Entities();
			Resource resource = new Resource();
			IndexExport index = new IndexExport();

			entity.Run(config.Entity, index);
			resource.Run(config.Resource, index);

			return 0;
		}
    }
}
