namespace XCommon.CodeGenerator.CSharp.Configuration
{
	public class CSharpConfig
    {
		public CSharpRepositoryConfig Repository { get; set; }

		public CSharpProjectConfig Factory { get; set; }

		public CSharpEntityFrameworkConfig EntityFramework { get; set; }

		public CSharpEntityConfig Entity { get; set; }

		public CSharpProjectConfig UnitTest { get; set; }

	}
}
