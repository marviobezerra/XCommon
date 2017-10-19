using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.Core
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
