using XCommon.CodeGeneratorV2.Core.DataBase;
using XCommon.CodeGeneratorV2.Core.DataBase.Implementation;
using XCommon.CodeGeneratorV2.CSharp;
using XCommon.CodeGeneratorV2.CSharp.Implementation;
using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2
{
	internal static class Factory
    {
		internal static void Do(GeneratorConfig config)
		{
			Kernel.Map<GeneratorConfig>().To(config);

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

		}

		private static void MapDataBaseReader()
		{
			Kernel.Map<IDataBaseRead>().To<DataBaseRead>();
		}

		private static void MapCSharp()
		{
			Kernel.Map<ICSharpRepositoryWritter>().To<CSharpRepositoryWritter>();
			Kernel.Map<ICSharpDataWritter>().To<CSharpDataWritter>();
			Kernel.Map<ICSharpEntityWritter>().To<CSharpEntityWritter>();
			Kernel.Map<ICSharpFactoryWritter>().To<CSharpFactoryWritter>();
			Kernel.Map<ICSharpUnitTestWritter>().To<CSharpUnitTestWritter>();
		}
	}
}
