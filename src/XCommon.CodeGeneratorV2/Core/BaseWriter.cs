using XCommon.Patterns.Ioc;

namespace XCommon.CodeGeneratorV2.Core
{
	public abstract class BaseWriter
	{
		[Inject]
		protected GeneratorConfig Config { get; set; }

		[Inject]
		protected IWriter Writer { get; set; }

		public BaseWriter()
		{
			Kernel.Resolve(this);
		}
	}
}
