using XCommon.CodeGerator.Angular.Writter;
using XCommon.CodeGerator.TypeScript.Configuration;
using XCommon.CodeGerator.TypeScript.Writter;

namespace XCommon.CodeGerator.TypeScript
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
