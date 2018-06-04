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
		[Inject]
		private ITypeScriptIndexExport Index { get; set; }

		private Dictionary<string, List<ResourceEntity>> Resources { get; set; }

		public void Run()
		{
			var builder = new StringBuilderIndented();

			builder
				.AppendLine("import { Injectable } from '@angular/core';")
				.AppendLine("import { HttpClient } from '@angular/common/http';")
				.AppendLine("import { Observable } from 'rxjs';")
				.AppendLine("");

			InitResource();
			LoadResouces();
			BuildResourceClasses(builder);
			BuidService(builder);

			Writer.WriteFile(Config.TypeScript.Resource.Path, Config.TypeScript.Resource.File, builder, true);

			Index.Run(Config.TypeScript.Resource.Path);
		}

		private void InitResource()
		{
			Resources = new Dictionary<string, List<ResourceEntity>>();

			Config.TypeScript.Resource.Cultures.ForEach(culture =>
			{
				Resources.Add(culture.Code, new List<ResourceEntity>());
			});

			if (!Resources.ContainsKey(Config.TypeScript.Resource.CultureDefault.Code))
			{
				Resources.Add(Config.TypeScript.Resource.CultureDefault.Code, new List<ResourceEntity>());
			}
		}

		private void LoadResouces()
		{
			// Get the culture from the generator config
			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				var resource = new List<ResourceEntity>();

				// Get the mapped resource files
				foreach (var manager in Config.TypeScript.Resource.Resources)
				{
					var resouceItem = new ResourceEntity
					{
						ResourceName = manager.Key.Name,
						Properties = new Dictionary<string, string>()
					};		

					// Get the properties from the resouce file
					foreach (var item in manager.Key.GetProperties().Where(c => c.PropertyType == typeof(string)))
					{
						resouceItem.Properties.Add(item.Name, manager.Value.GetString(item.Name, new CultureInfo(culture.Code)));
					}

					resource.Add(resouceItem);
				}

				Resources[culture.Code] = resource;
			}
		}

		private void BuildResourceClasses(StringBuilderIndented builder)
		{
			var resources = Resources[Config.TypeScript.Resource.CultureDefault.Code];

			foreach (var resource in resources)
			{
				builder
					.AppendLine($"class {resource.ResourceName}Resource {{")
					.IncrementIndent();

				foreach (var value in resource.Properties)
				{
					builder
						.AppendLine($"{value.Key} = '{value.Value.Replace("'", "\\'")}';");
				}

				builder
					.DecrementIndent()
					.AppendLine("}")
					.AppendLine()
					.DecrementIndent();
			}
		}

		private string BuidService(StringBuilderIndented builder)
		{
			builder
				.AppendLine("@Injectable({")
				.IncrementIndent()
				.AppendLine("providedIn: 'root'")
				.DecrementIndent()
				.AppendLine("})")
				.AppendLine($"export class {Config.TypeScript.Resource.ServiceName} {{")
				.IncrementIndent()
				.AppendLine("");

			foreach (var resource in Resources[Config.TypeScript.Resource.CultureDefault.Code])
			{
				builder
					.AppendLine($"public {resource.ResourceName} = new {resource.ResourceName}Resource();")
					.AppendLine("");
			}

			builder
				.AppendLine("public CurrentCulture = {")
				.IncrementIndent()
				.AppendLine($"Code: '{Config.TypeScript.Resource.CultureDefault.Code}',")
				.AppendLine($"Name: '{Config.TypeScript.Resource.CultureDefault.Name}'")
				.DecrementIndent()
				.AppendLine("};")
				.AppendLine();

			builder
				.AppendLine("public Cultures = [")
				.IncrementIndent();

			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				builder.AppendLine($"{{ Code: '{culture.Code}', Name: '{culture.Name}' }}");
			}

			builder
				.DecrementIndent()
				.AppendLine("];")
				.AppendLine("");


			builder
				.AppendLine("constructor(private http: HttpClient) {")
				.AppendLine("}")
				.AppendLine("");

			builder
				.AppendLine("public setCulture(cultureCode: string): Observable<boolean> {")
				.AppendLine("")
				.IncrementIndent()
				.AppendLine("const newCulture = this.Cultures.find(c => c.Code === cultureCode);")
				.AppendLine("")
				.AppendLine("if (!newCulture) {")
				.IncrementIndent()
				.AppendLine("throw new Error(`Invalid culture: ${cultureCode}`);")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine("")
				.AppendLine("return new Observable<boolean>(observer => {")
				.AppendLine("")
				.IncrementIndent()
				.AppendLine("if (cultureCode === this.CurrentCulture.Code) {")
				.IncrementIndent()
				.AppendLine("observer.next(true);")
				.AppendLine("observer.complete();")
				.AppendLine("return;")
				.DecrementIndent()
				.AppendLine("}")
				.AppendLine("")
				.AppendLine($"this.http.post<MessagesResource>('{Config.TypeScript.Resource.RequestAddress}', cultureCode)")
				.IncrementIndent()
				.AppendLine(".subscribe(res => {")
				.IncrementIndent()
				.AppendLine("this.Messages = res;")
				.AppendLine("observer.next(true);")
				.AppendLine("observer.complete();")
				.DecrementIndent()
				.AppendLine("});")
				.DecrementIndent()
				.DecrementIndent()
				.AppendLine("});")
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent();

			builder
				.DecrementIndent()
				.AppendLine("}");

			return builder.ToString();
		}
	}
}
