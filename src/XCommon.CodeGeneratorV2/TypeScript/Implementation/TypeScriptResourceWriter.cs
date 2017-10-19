using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.CodeGenerator.TypeScript.Implementation.Helper;
using XCommon.Patterns.Ioc;
using XCommon.Util;

namespace XCommon.CodeGenerator.TypeScript.Implementation
{
	public class TypeScriptResourceWriter : BaseWriter, ITypeScriptResourceWriter
	{
		public void Run()
		{
			var builder = new StringBuilderIndented();

			builder
				.AppendLine("import { Injectable } from '@angular/core';");

			if (Config.TypeScript.Resource.LazyLoad)
			{
				builder
					.AppendLine("import { Http, Response } from '@angular/http';")
					.AppendLine("import { Observable } from 'rxjs/Observable';");
			}

			builder
				.AppendLine($"import {{ Map }} from '{Config.TypeScript.Resource.EntityPath}';")
				.AppendLine();

			Resources = new List<GeneratorResourceEntity>();

			GetResouces();
			BuildLanguageSuported(builder);
			BuildInterface(builder);

			if (!Config.TypeScript.Resource.LazyLoad)
			{
				BuildClass(builder);
			}

			BuidService(builder);
			
			var file = Config.TypeScript.Resource.File.GetSelector() + ".service.ts";
			var path = Config.TypeScript.Resource.Path.ToLower();
			Writer.WriteFile(path, file, builder, true);

			Index.Run(path);
		}

		[Inject]
		private ITypeScriptIndexExport Index { get; set; }

		private List<GeneratorResourceEntity> Resources { get; set; }

		private string GetCultureName(string culture)
		{
			return culture
				.Replace("-", string.Empty)
				.ToUpper();
		}

		private void GetResouces()
		{
			foreach (var manager in Config.TypeScript.Resource.Resources)
			{
				var resource = new GeneratorResourceEntity
				{
					ResourceName = manager.Key.Name
				};

				foreach (var culture in Config.TypeScript.Resource.Cultures)
				{
					var retorno = new Dictionary<string, string>();

					foreach (var item in manager.Key.GetProperties().Where(c => c.PropertyType == typeof(string)))
					{
						retorno.Add(item.Name, manager.Value.GetString(item.Name, new CultureInfo(culture.Name)));
					}

					resource.Values.Add(new GeneratorResourceEntityValues { Culture = culture.Name, Properties = retorno });
				}

				Resources.Add(resource);
			}
		}

		private void BuildInterface(StringBuilderIndented builder)
		{
			foreach (var resource in Resources)
			{
				builder
					.AppendLine($"export interface I{resource.ResourceName} {{")
					.IncrementIndent();

				foreach (var value in resource.Values.FirstOrDefault().Properties)
				{
					builder
						.AppendLine($"{value.Key}: string;");
				}

				builder
					.DecrementIndent()
					.AppendLine("}")
					.AppendLine();
			}

			builder
				.AppendLine("export interface IResource {")
				.IncrementIndent()
				.AppendLine("Culture: string;");

			foreach (var resource in Resources)
			{
				builder
					.AppendLine($"{resource.ResourceName}: I{resource.ResourceName};");
			}

			builder
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();
		}

		private void BuildClass(StringBuilderIndented builder)
		{
			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				foreach (var resource in Resources)
				{
					builder
						.AppendLine($"class {resource.ResourceName}{GetCultureName(culture.Name)} implements I{resource.ResourceName} {{")
						.IncrementIndent();

					foreach (var property in resource.Values.Where(c => c.Culture == culture.Name).SelectMany(c => c.Properties))
					{
						builder
							.AppendLine($"{property.Key}: string = '{property.Value}';");
					}

					builder
						.DecrementIndent()
						.AppendLine("}")
						.AppendLine();
				}
			}
		}

		private string BuidService(StringBuilderIndented builder)
		{
			builder
				.AppendLine("@Injectable()")
				.AppendLine("export class ResourceService {")
				.IncrementIndent()
				.AppendLine(Config.TypeScript.Resource.LazyLoad ? "constructor(private http: Http) {" : "constructor() {")
				.IncrementIndent();

			BuildLanguageSuportedContructor(builder);

			builder
				.AppendLine($"this.SetLanguage('{Config.TypeScript.Resource.CultureDefault.Name}')")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();

			if (Config.TypeScript.Resource.LazyLoad)
			{
				builder
					.AppendLine("private Resources: Map<IResource> = {};");
			}

			builder
				.AppendLine("public Languages: ApplicationCulture[] = [];")
				.AppendLine("private Language: ApplicationCulture;")
				.AppendLine();

			foreach (var resource in Resources)
			{
				builder.AppendLine($"public {resource.ResourceName}: I{resource.ResourceName};");
			}

			builder
				.AppendLine()
				.AppendLine("public GetLanguage(): ApplicationCulture {")
				.IncrementIndent()
				.AppendLine("return this.Language;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine("public SetLanguage(language: string): void {")
				.IncrementIndent()
				.AppendLine()

				.AppendLine("if (this.Language && this.Language.Name === language) {")
				.IncrementIndent()
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()

				.AppendLine("let value = this.Languages.find((item: ApplicationCulture) => item.Name == language);")
				.AppendLine()
				.AppendLine("if (!value) {")
				.IncrementIndent()
				.AppendLine($"console.warn(`Unknown language: ${{language}}! Set up current culture as the default language: {Config.TypeScript.Resource.CultureDefault.Name}`);")
				.AppendLine($"this.SetLanguage('{Config.TypeScript.Resource.CultureDefault.Name}');")
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine("this.Language = value;")
				.AppendLine();


			if (!Config.TypeScript.Resource.LazyLoad)
			{
				BuildNormalService(builder);
			}

			if (Config.TypeScript.Resource.LazyLoad)
			{
				BuildLazyService(builder);
			}

			builder
				.DecrementIndent()
							.AppendLine("}")
							.DecrementIndent()
							.AppendLine("}");

			return builder.ToString();
		}

		private void BuildLazyService(StringBuilderIndented builder)
		{
			builder
				.AppendLine("if (this.Resources[this.Language.Name]) {")
				.IncrementIndent()
				.AppendLine("let resource: IResource = this.Resources[this.Language.Name];")
				.AppendLine();

			foreach (var resource in Resources)
			{
				builder
					.AppendLine($"this.{resource.ResourceName} = resource.{resource.ResourceName};");
			}

			builder
				.AppendLine()
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine($"this.http.get(`{Config.TypeScript.Resource.RequestAddress}/${{this.Language.Name}}`)")
				.IncrementIndent()
				.AppendLine(".map((response: Response, index: number) => response.json())")
				.AppendLine(".subscribe((resource: IResource) => {")
				.IncrementIndent()
				.AppendLine("this.Resources[resource.Culture] = resource;")
				.AppendLine();

			foreach (var resource in Resources)
			{
				builder
					.AppendLine($"this.{resource.ResourceName} = resource.{resource.ResourceName};");
			}

			builder
				.DecrementIndent()
				.AppendLine("});");
		}

		private void BuildNormalService(StringBuilderIndented builder)
		{
			builder
				.AppendLine("switch (this.Language.Name) {")
				.IncrementIndent();

			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				builder
					.AppendLine($"case '{culture.Name}':")
					.IncrementIndent();

				foreach (var resource in Resources)
				{
					builder
						.AppendLine($"this.{resource.ResourceName} = new {resource.ResourceName}{GetCultureName(culture.Name)}();");
				}

				builder
					.AppendLine("break;")
					.DecrementIndent();
			}

			builder
				.DecrementIndent()
				.AppendLine("}");
		}

		private void BuildLanguageSuportedContructor(StringBuilderIndented builder)
		{
			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				builder.AppendLine($"this.Languages.push({{ Name: '{culture.Name}', Description: '{culture.Description}' }});");
			}
		}

		private void BuildLanguageSuported(StringBuilderIndented builder)
		{
			builder
				.AppendLine("export class ApplicationCulture {")
				.IncrementIndent()
				.AppendLine("public Name: string;")
				.AppendLine("public Description: string;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();
		}
	}
}
