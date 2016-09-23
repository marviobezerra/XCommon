using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using XCommon.CodeGerator.Angular.Extensions;
using XCommon.CodeGerator.Angular.Writter;
using XCommon.CodeGerator.Core.Util;
using XCommon.CodeGerator.TypeScript.Configuration;
using XCommon.Util;

namespace XCommon.CodeGerator.TypeScript
{
	public class Resource : FileWriter
	{
		private IndexExport Index { get; set; } = new IndexExport();
		
		private List<GeneratorResourceEntity> Resources { get; set; }
		
		internal void Run(TypeScriptResource config, IndexExport index)
		{
			if (config.Resources == null || config.Resources.Count <= 0)
				return;

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.AppendLine("import { Injectable } from \"@angular/core\";");

			if (config.LazyLoad)
			{
				builder
					.AppendLine("import { Http, Response } from \"@angular/http\";")
					.AppendLine("import { Observable } from \"rxjs/Observable\";");
			}

			builder
				.AppendLine("import { Map } from \"../entity\";")
				.AppendLine();

			Resources = new List<GeneratorResourceEntity>();

			GetResouces(config);
			BuildLanguageSuported(builder);
			BuildInterface(builder);

			if (!config.LazyLoad)
				BuildClass(config, builder);

			BuidService(config, builder);

			if (!Directory.Exists(config.Path))
				Directory.CreateDirectory(config.Path);

			var file = config.File.GetSelector() + ".service.ts";

			WriteFile(config.Path.ToLower(), file, builder);

			Index.Run(config.Path);

			index.Run(config.Path);
			Console.WriteLine("Generate resource typescript - OK");
		}

		private string GetCultureName(string culture)
		{
			return culture
				.Replace("-", string.Empty)
				.ToUpper();
		}

		private void GetResouces(TypeScriptResource config)
		{
			foreach (var manager in config.Resources)
			{
				GeneratorResourceEntity resource = new GeneratorResourceEntity
				{
					ResourceName = manager.Key.Name
				};

				foreach (var culture in config.Cultures)
				{
					Dictionary<string, string> retorno = new Dictionary<string, string>();

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

		private void BuildClass(TypeScriptResource config, StringBuilderIndented builder)
		{
			foreach (var culture in config.Cultures)
			{
				foreach (var resource in Resources)
				{
					builder
						.AppendLine($"class {resource.ResourceName}{GetCultureName(culture.Name)} implements I{resource.ResourceName} {{")
						.IncrementIndent();

					foreach (var property in resource.Values.Where(c => c.Culture == culture.Name).SelectMany(c => c.Properties))
					{
						builder
							.AppendLine($"{property.Key}: string = \"{property.Value}\";");
					}

					builder
						.DecrementIndent()
						.AppendLine("}")
						.AppendLine();
				}
			}
		}

		private string BuidService(TypeScriptResource config, StringBuilderIndented builder)
		{
			builder
				.AppendLine("@Injectable()")
				.AppendLine("export class ResourceService {")
				.IncrementIndent()
				.AppendLine(config.LazyLoad ? "constructor(private http: Http) {" : "constructor() {")
				.IncrementIndent();

			BuildLanguageSuportedContructor(config, builder);

			builder
				.AppendLine($"this.SetLanguage(\"{config.CultureDefault.Name}\")")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();

			if (config.LazyLoad)
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
				.AppendLine($"console.warn(`Unknown language: ${{language}}! Set up current culture as the default language: {config.CultureDefault.Name}`);")
				.AppendLine($"this.SetLanguage(\"{config.CultureDefault.Name}\");")
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine("this.Language = value;")
				.AppendLine();


			if (!config.LazyLoad)
			{
				BuildNormalService(config, builder);
			}

			if (config.LazyLoad)
			{
				BuildLazyService(config, builder);
			}

			builder
				.DecrementIndent()
							.AppendLine("}")
							.DecrementIndent()
							.AppendLine("}");

			return builder.ToString();
		}

		private void BuildLazyService(TypeScriptResource config, StringBuilderIndented builder)
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
				.AppendLine($"this.http.get(`{config.RequestAddress}/${{this.Language.Name}}`)")
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

		private void BuildNormalService(TypeScriptResource config, StringBuilderIndented builder)
		{
			builder
				.AppendLine("switch (this.Language.Name) {")
				.IncrementIndent();

			foreach (var culture in config.Cultures)
			{
				builder
					.AppendLine($"case \"{culture.Name}\":")
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

		private void BuildLanguageSuportedContructor(TypeScriptResource config, StringBuilderIndented builder)
		{
			foreach (var culture in config.Cultures)
			{
				builder.AppendLine($"this.Languages.push({{ Name: \"{culture.Name}\", Description: \"{culture.Description}\" }});");
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
