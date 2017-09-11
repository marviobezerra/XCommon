namespace XCommon.CodeGenerator.Angular.Configuration
{
	public class AngularConfig
    {
		public AngularConfig()
		{
			QuoteType = QuoteType.Double;
		}

		public QuoteType QuoteType { get; set; }

		public string Path { get; set; }
	}
}
