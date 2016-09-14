using XCommon.CodeGerator.CSharp.Configuration;
using XCommon.CodeGerator.CSharp.Writter;

namespace XCommon.CodeGerator.CSharp
{
	internal class CSharpRunner
    {
		internal int Run(CSharpConfig config)
		{
			if (config == null)
				return -1;

			Concrete concrete = new Concrete();
			Contract contract = new Contract();
			Data data = new Data();
			Entity entity = new Entity();
			Factory factory = new Factory();

			data.Run(config);
			entity.Run(config);
			contract.Run(config);
			concrete.Run(config);
			factory.Run(config);

			return 0;
		}
	}
}
