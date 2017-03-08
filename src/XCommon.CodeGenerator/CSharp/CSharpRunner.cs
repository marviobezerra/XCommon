using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.CSharp.Writter;

namespace XCommon.CodeGenerator.CSharp
{
	internal class CSharpRunner
    {
		internal int Run(CSharpConfig config)
		{
			if (config == null)
			{
				return -1;
			}

			Concrete concrete = new Concrete();
			Contract contract = new Contract();
			Data data = new Data();
			Entity entity = new Entity();
			Factory factory = new Factory();
			Writter.UnitTest unitTest = new Writter.UnitTest();

			data.Run(config);
			entity.Run(config);
			contract.Run(config);
			concrete.Run(config);
			unitTest.Run(config);
			factory.Run(config);

			return 0;
		}
	}
}
