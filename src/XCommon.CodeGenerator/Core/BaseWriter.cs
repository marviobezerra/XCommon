using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.Core
{
	public abstract class BaseWriter
	{
		[Inject]
		protected GeneratorConfig Config { get; set; }

		[Inject]
		protected IWriter Writer { get; set; }

		protected string Quote
		{
			get
			{
				return Config.TypeScript.QuoteType == QuoteType.Double
					? "\""
					: "'";
			}
		}

		public BaseWriter()
		{
			Kernel.Resolve(this);
		}
	}
}
