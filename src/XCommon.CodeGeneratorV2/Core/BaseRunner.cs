using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2.Core
{
	internal abstract class BaseRunner
    {
		[Inject]
		protected GeneratorConfig Config { get; set; }

		public BaseRunner()
		{
			Kernel.Resolve(this);
		}
    }
}
