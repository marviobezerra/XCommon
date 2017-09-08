namespace XCommon.CodeGeneratorV2.CSharp.Configuration
{
	public class CSharpProjectConfig
    {
		public CSharpProjectConfig()
		{
			Execute = true;
		}

		public bool Execute { get; set; }

		public string Path { get; set; }

		public string NameSpace { get; set; }
	}
}
