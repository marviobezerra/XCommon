namespace XCommon.CodeGenerator.CSharp.Configuration
{
	public class CSharpRepositoryConfig
    {
		public CSharpRepositoryConfig()
		{
			Execute = true;
		}

		public bool Execute { get; set; }

		public CSharpProjectConfig Contract { get; set; }

		public CSharpProjectConfig Concrecte { get; set; }
	}
}
