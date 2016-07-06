using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using XCommon.CodeGerator.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.TypeScript
{
	public class Resource
	{
		private Configuration.ConfigResource Config => Generator.Configuration.Resource;


		private List<GeneratorResourceEntity> Resources { get; set; }
		
		internal void Run()
		{
			if (Config.Resources == null || Config.Resources.Count <= 0)
				return;

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.AppendLine("import { Injectable } from '@angular/core';")
				.AppendLine();

			Resources = new List<GeneratorResourceEntity>();

			GetResouces();
			BuildLanguageSuported(builder);			
			BuildInterface(builder);
			BuildClass(builder);
			BuidService(builder);

			if (!Directory.Exists(Config.Path))
				Directory.CreateDirectory(Config.Path);

			var file = Path.Combine(Config.Path, Config.File);

			File.WriteAllText(file.GetSelector(), builder.ToString(), Encoding.UTF8);
			Console.WriteLine("Typescript resource messages now are up to date");
		}

		private string GetCultureName(string culture)
		{
			return culture
				.Replace("-", string.Empty);
		}

		private void GetResouces()
		{
			foreach (var manager in Config.Resources)
			{
				GeneratorResourceEntity resource = new GeneratorResourceEntity
				{
					ResourceName = manager.Key.Name
				};

				foreach (var culture in Config.Cultures)
				{
					Dictionary<string, string> retorno = new Dictionary<string, string>();

					foreach (var item in manager.Key.GetProperties().Where(c => c.PropertyType == typeof(string)))
					{
						retorno.Add(item.Name, manager.Value.GetString(item.Name, new CultureInfo(culture)));
					}

					resource.Values.Add(new GeneratorResourceEntityValues { Culture = culture, Properties = retorno });
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
		}

		private void BuildClass(StringBuilderIndented builder)
		{
			foreach (var culture in Config.Cultures)
			{
				foreach (var resource in Resources)
				{
					builder
						.AppendLine($"class {resource.ResourceName}{GetCultureName(culture)} implements I{resource.ResourceName} {{")
						.IncrementIndent();

					foreach (var property in resource.Values.Where(c => c.Culture == culture).SelectMany(c => c.Properties))
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

		private string BuidService(StringBuilderIndented builder)
		{
			builder
				.AppendLine("@Injectable()")
				.AppendLine("export class ResourceService {")
				.IncrementIndent()
				.AppendLine("constructor() {")
				.IncrementIndent()
				.AppendLine($"this.SetLanguage(LanguageSuported.{GetCultureName(Config.CultureDefault)})")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine("private Language: LanguageSuported;")
				.AppendLine("OnCultureChange: Array<(name: string) => void> = [];")
				.AppendLine();

			foreach (var resource in Resources)
			{
				builder.AppendLine($"{resource.ResourceName}: I{resource.ResourceName};");
			}

			builder
				.AppendLine()
				.AppendLine("GetLanguage(): LanguageSuported {")
				.IncrementIndent()
				.AppendLine("return this.Language;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine()
				.AppendLine("SetLanguage(language: LanguageSuported): void {")
				.IncrementIndent()
				.AppendLine("this.Language = language;")
				.AppendLine("this.OnCultureChange.forEach(notify => notify(LanguageSuported[language]));")
				.AppendLine()
				.AppendLine("switch (this.Language) {")
				.IncrementIndent();

			foreach (var culture in Config.Cultures)
			{
				builder
					.AppendLine($"case LanguageSuported.{GetCultureName(culture)}:")
					.IncrementIndent();

				foreach (var resource in Resources)
				{
					builder
						.AppendLine($"this.{resource.ResourceName} = new {resource.ResourceName}{GetCultureName(culture)}();");
				}

				builder
					.AppendLine("break;")
					.DecrementIndent();
			}

			builder
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent()
				.AppendLine("}");

			return builder.ToString();
		}

		private void BuildLanguageSuported(StringBuilderIndented builder)
		{


			builder
				.AppendLine("export enum LanguageSuported {")
				.IncrementIndent();

			var cultures = Resources.SelectMany(c => c.Values
				.Select(x => x.Culture))
				.Distinct()
				.OrderBy(c => c)
				.ToList();

			int count = 0;

			foreach (var culture in cultures)
			{
				count++;
				string item = GetCultureName(culture);
				item += cultures.Count == count ? "" : ",";
				builder.AppendLine($"{item}");
			}

			builder
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine();
		}
	}
}
