using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.Core
{
	public abstract class BaseRunner
    {
		[Inject]
		protected GeneratorConfig Config { get; set; }

		public BaseRunner()
		{
			Kernel.Resolve(this);
		}

		public abstract int Run();

	}
}
