using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2.Core
{
	internal abstract class BaseRunner
    {
		public BaseRunner()
		{
			Kernel.Resolve(this);
		}
    }
}
