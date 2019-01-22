using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using XCommon.Application.Settings;
using XCommon.CodeGenerator;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.TypeScript.Configuration;

namespace XCommon.ResourceExtractor
{
	internal class Program
	{
		private static string SolutionPath { get; set; }

		private static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("It requires exact one argument");
			}

			SolutionPath = args[0];
			var gen = new Generator(GetConfig());
			var command = new List<string> { "-t" };
			gen.Run(command.ToArray());
		}

		private static GeneratorConfig GetConfig()
		{
			return new GeneratorConfig
			{
				TypeScript = new TypeScriptConfig
				{
					QuoteType = QuoteType.Single,
					Entity = new TypeScriptEntityConfig
					{
						Path = Path.Combine(SolutionPath, @"model"),
						FilePrefix = "xc",
						FileSufix = "entity",
						NameOverride = new List<TypeScriptNameOverride>
						{
							new TypeScriptNameOverride("XCommon.Application.Authentication.Entity", "Auth")
						},
						Assemblys = new List<Assembly>(),
						TypesExtra = new List<Type>
						{
							typeof(Application.Authentication.Entity.ProviderType),
							typeof(Application.Authentication.Entity.PasswordChangeEntity),
							typeof(Application.Authentication.Entity.PasswordRecoveryEntity),
							typeof(Application.Authentication.Entity.SignInEntity),
							typeof(Application.Authentication.Entity.SignUpEntity),
							typeof(Application.Authentication.Entity.TicketEntity),

							typeof(Entity.Register.Enumerators.GenderType),

							typeof(Entity.Register.Filter.PeopleFilter),
							typeof(Entity.Register.Filter.UsersFilter),
							typeof(Entity.Register.Filter.UsersProvidersFilter),
							typeof(Entity.Register.Filter.UsersRolesFilter),
							typeof(Entity.Register.Filter.UsersTokensFilter),

							typeof(Entity.Register.PeopleEntity),
							typeof(Entity.Register.UsersEntity),
							typeof(Entity.Register.UsersProvidersEntity),
							typeof(Entity.Register.UsersRolesEntity),
							typeof(Entity.Register.UsersTokensEntity)

						}
					},
					Resource = new TypeScriptResourceConfig
					{
						Path = Path.Combine(SolutionPath, @"app-core\src\lib\services"),
						PathJson = Path.Combine(SolutionPath, @"assets\localization"),
						JsonPrefix = "XCommon",
						Extractor = true,
						File = "xc-translation.service.ts",
						RequestAddress = "/assets/localization/",
						ServiceName = "XcTranslation",
						Execute = true,
						CultureDefault = new ApplicationCulture { Name = "English", Code = "en-US" },
						Cultures = new List<ApplicationCulture>
						{
							new ApplicationCulture { Name = "English", Code = "en-US" },
							new ApplicationCulture { Name = "Portugues", Code = "pt-BR" }
						},
						Resources = new Dictionary<Type, ResourceManager>
						{
							{ typeof(Resources.Messages), Resources.Messages.ResourceManager },
							{ typeof(Resources.Authentication), Resources.Authentication.ResourceManager },
							{ typeof(Resources.Labels), Resources.Labels.ResourceManager }
						}

					}
				}
			};
		}

	}
}
