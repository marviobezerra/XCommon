using System.Collections.Generic;

namespace XCommon.CodeGenerator.CSharp.Configuration
{
	public class CSharpConfig
	{
		public CSharpConfig()
		{
			ApplicationClasses = new List<CSharpApplicationClass>
			{
				new CSharpApplicationClass("Register", "People"),
				new CSharpApplicationClass("Register", "Users"),
				new CSharpApplicationClass("Register", "UsersProviders"),
				new CSharpApplicationClass("Register", "UsersRoles"),
				new CSharpApplicationClass("Register", "UsersTokens"),
				new CSharpApplicationClass("System", "Configs")
			};
		}


		public List<CSharpApplicationClass> ApplicationClasses { get; set; }

		public bool UsingApplicationBase { get; set; }

		public CSharpRepositoryConfig Repository { get; set; }

		public CSharpProjectConfig Factory { get; set; }

		public CSharpEntityFrameworkConfig EntityFramework { get; set; }

		public CSharpEntityConfig Entity { get; set; }

		public CSharpProjectConfig UnitTest { get; set; }


		
	}
}
