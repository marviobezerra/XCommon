using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.Core.DataBase.Implementation;
using XCommon.CodeGenerator.Core.Implementation;
using XCommon.CodeGenerator.CSharp;
using XCommon.CodeGenerator.CSharp.Implementation;
using XCommon.CodeGenerator.TypeScript;
using XCommon.CodeGenerator.TypeScript.Implementation;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator
{
	internal static class Factory
    {
		internal static void Do(GeneratorConfig config)
		{
			Kernel.Map<GeneratorConfig>().To(config);
			Kernel.Map<IWriter>().To<FileWriter>();

			MapCSharp();
			MapDataBaseReader();
			MapTypeScript();
		}

		private static void MapTypeScript()
		{
			Kernel.Map<ITypeScriptIndexExport>().To<TypeScriptIndexExport>();
			Kernel.Map<ITypeScriptEntityWriter>().To<TypeScriptEntityWriter>();
			Kernel.Map<ITypeScriptResourceWriter>().To<TypeScriptResourceWriter>();
		}

		private static void MapDataBaseReader()
		{
			Kernel.Map<IDataBaseRead>().To<DataBaseRead>();
		}

		private static void MapCSharp()
		{
			Kernel.Map<ICSharpRepositoryWriter>().To<CSharpRepositoryWritter>();
			Kernel.Map<ICSharpEnityFrameworkWriter>().To<CSharpEnityFrameworkWriter>();
			Kernel.Map<ICSharpEntityWriter>().To<CSharpEntityWriter>();
			Kernel.Map<ICSharpFactoryWriter>().To<CSharpFactoryWritter>();
			Kernel.Map<ICSharpUnitTestWriter>().To<CSharpUnitTestWritter>();
		}
	}
}
