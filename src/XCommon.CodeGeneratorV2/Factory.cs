using XCommon.CodeGeneratorV2.Core;
using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.Core.DataBase.Implementation;
using XCommon.CodeGeneratorV2.Core.Implementation;
using XCommon.CodeGeneratorV2.CSharp;
using XCommon.CodeGeneratorV2.CSharp.Implementation;
using XCommon.CodeGeneratorV2.TypeScript;
using XCommon.CodeGeneratorV2.TypeScript.Implementation;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2
{
	internal static class Factory
    {
		internal static void Do(GeneratorConfig config)
		{
			Kernel.Map<GeneratorConfig>().To(config);
			Kernel.Map<IWriter>().To<FileWriter>();

			MapCSharp();
			MapDataBaseReader();
			MapAngular();
			MapTypeScript();
		}

		private static void MapAngular()
		{

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
