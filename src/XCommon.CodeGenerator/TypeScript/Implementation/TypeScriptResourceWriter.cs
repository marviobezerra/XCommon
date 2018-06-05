using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using Newtonsoft.Json;
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

			InitResource();
			WriteResourceClasses(builder);
			WriteService(builder);
			WriteJsonFiles();

			Writer.WriteFile(Config.TypeScript.Resource.Path, Config.TypeScript.Resource.File, builder, true);


			Index.Run(Config.TypeScript.Resource.Path);
		}

		private void WriteJsonFiles()
		{
			foreach (var culture in Resources)
			{
				dynamic result = new ExpandoObject();

				foreach (var resouce in culture.Value)
				{
					dynamic current = new ExpandoObject();

					foreach (var item in resouce.Properties)
					{
						(current as IDictionary<string, object>).Add(item.Key, item.Value);
					}

					(result as IDictionary<string, object>).Add(resouce.ResourceName, current);
				}

				Writer.WriteFile(Config.TypeScript.Resource.PathJson, $"{culture.Key}.json", JsonConvert.SerializeObject(result), true);

			}
		}

		private void InitResource()
		{
			// Load the maped resource files
			Resources = new Dictionary<string, List<ResourceEntity>>();

			Config.TypeScript.Resource.Cultures.ForEach(culture =>
			{
				Resources.Add(culture.Code, new List<ResourceEntity>());
			});

			// Check if the default is in the list
			if (!Resources.ContainsKey(Config.TypeScript.Resource.CultureDefault.Code))
			{
				Resources.Add(Config.TypeScript.Resource.CultureDefault.Code, new List<ResourceEntity>());
			}

			// Get the culture from the generator config
			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				var resource = new List<ResourceEntity>();
				var cultureInfo = new CultureInfo(culture.Code);

				// Get the mapped resource files
				foreach (var manager in Config.TypeScript.Resource.Resources)
				{
					var resouceItem = new ResourceEntity
					{
						ResourceName = manager.Name,
						Properties = new Dictionary<string, string>()
					};

					var rm = new ResourceManager(manager);

					using (var resourceSet = rm.GetResourceSet(cultureInfo, true, true))
					{
						resouceItem.Properties = resourceSet.OfType<DictionaryEntry>()
								 .ToDictionary(r => r.Key.ToString(), r => r.Value.ToString());
					}

					resource.Add(resouceItem);
				}

				Resources[culture.Code] = resource;
			}
		}

		private void WriteResourceClasses(StringBuilderIndented builder)
		{
			var resources = Resources[Config.TypeScript.Resource.CultureDefault.Code];

			// Write the file header
			builder
				.AppendLine("import { Injectable } from '@angular/core';")
				.AppendLine("import { HttpClient } from '@angular/common/http';")
				.AppendLine("import { Observable } from 'rxjs';")
				.AppendLine("");

			// Create the resouce class with default values
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

		private string WriteService(StringBuilderIndented builder)
		{
			// Service declaration
			builder
				.AppendLine("@Injectable({")
				.IncrementIndent()
				.AppendLine("providedIn: 'root'")
				.DecrementIndent()
				.AppendLine("})")
				.AppendLine($"export class {Config.TypeScript.Resource.ServiceName} {{")
				.IncrementIndent()
				.AppendLine("");

			// Resource Propertoes
			foreach (var resource in Resources[Config.TypeScript.Resource.CultureDefault.Code])
			{
				builder
					.AppendLine($"public {resource.ResourceName} = new {resource.ResourceName}Resource();")
					.AppendLine("");
			}

			// Current Culture
			builder
				.AppendLine("public CurrentCulture = {")
				.IncrementIndent()
				.AppendLine($"Code: '{Config.TypeScript.Resource.CultureDefault.Code}',")
				.AppendLine($"Name: '{Config.TypeScript.Resource.CultureDefault.Name}'")
				.DecrementIndent()
				.AppendLine("};")
				.AppendLine();

			// Available cultures
			builder
				.AppendLine("public Cultures = [")
				.IncrementIndent();

			foreach (var culture in Config.TypeScript.Resource.Cultures)
			{
				var last = Config.TypeScript.Resource.Cultures.Last().Code.Equals(culture.Code);
				var comma = last ? string.Empty : ",";
				builder.AppendLine($"{{ Code: '{culture.Code}', Name: '{culture.Name}' }}{comma}");
			}

			builder
				.DecrementIndent()
				.AppendLine("];")
				.AppendLine("");

			// Service constructor
			builder
				.AppendLine("constructor(private http: HttpClient) {")
				.AppendLine("}")
				.AppendLine("");

			// Set culture method
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
				.AppendLine($"this.http.get<any>(`{Config.TypeScript.Resource.RequestAddress}${{cultureCode}}.json`)")
				.IncrementIndent()
				.AppendLine(".subscribe(res => {")
				.IncrementIndent();

			// Set the properties with the new language

			foreach (var item in Resources[Config.TypeScript.Resource.CultureDefault.Code])
			{
				builder
					.AppendLine($"this.{item.ResourceName} = res.{item.ResourceName};");
			}

			// Complete the service
			builder
				.AppendLine("")
				.AppendLine("this.CurrentCulture = newCulture;")
				.AppendLine("")
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

			// Close service class
			builder
				.DecrementIndent()
				.AppendLine("}");

			return builder.ToString();
		}
	}
}
