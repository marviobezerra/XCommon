using XCommon.Patterns.Ioc;

namespace XCommon.CodeGenerator.Core
{
	public abstract class BaseWriter
	{
		[Inject]
		protected GeneratorConfig Config { get; private set; }

		[Inject]
		protected IFileWriter Writer { get; private set; }

		[Inject]
		protected ILog Log { get; private set; }

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

		public abstract bool Write();
	}
}
