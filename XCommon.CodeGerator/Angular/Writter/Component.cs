using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using XCommon.CodeGerator.Angular.Extensions;
using XCommon.CodeGerator.Core.Util;
using XCommon.Util;

namespace XCommon.CodeGerator.Angular.Writter
{
	internal class Component : FileWriter
	{
		internal void Run(string path, string feature, string htmlRoot, List<string> styleInclude, List<string> components)
		{
			path = path.ToLower();

			foreach (string component in components)
			{
				var outlet = component.GetOutLet();
				var componentName = component.Replace("?o", string.Empty);
				var selector = componentName.GetSelector();
				
				if (!Regex.IsMatch(componentName, @"^[a-zA-Z]+$"))
				{
					Console.WriteLine($"Invalid component name: {component}");
					continue;
				}

				TypeScript(path, componentName, selector, htmlRoot, feature);
				Sass(path, componentName, selector, styleInclude);
				Html(path, componentName, selector, outlet);

				Console.WriteLine($"Generated component {selector}");
			}
		}

		private void TypeScript(string path, string name, string selector, string htmlRoot, string feture)
		{
			var file = $"{selector}.component.ts";

			if (File.Exists(Path.Combine(path, file)))
			{
				Console.WriteLine($"File already exists: {file}");
				return;
			}

			StringBuilderIndented builder = new StringBuilderIndented();
			string templateUrl = $"\"{htmlRoot}/{feture}/{selector}.html\",";

			builder
				.AppendLine("import { Component, OnInit } from \"@angular/core\";")
				.AppendLine()
				.AppendLine("@Component({")
				.IncrementIndent()
				.AppendLine($"selector: \"{selector}\",")
				.AppendLine("templateUrl: " + templateUrl.ToLower())
				.AppendLine($"styles: [require(\"./{selector}.scss\")]")
				.DecrementIndent()
				.AppendLine("})")
				.AppendLine($"export class {name}Component implements OnInit {{")
				.IncrementIndent()
				.AppendLine("constructor() { }")
				.AppendLine()
				.AppendLine("ngOnInit(): void { }")
				.DecrementIndent()
				.AppendLine("}");

			WriteFile(path, file, builder);
		}

		private void Sass(string path, string name, string selector, List<string> styleInclude)
		{
			var file = $"{selector}.scss";

			if (File.Exists(Path.Combine(path, file)))
			{
				Console.WriteLine($"File already exists: {file}");
				return;
			}

			StringBuilderIndented builder = new StringBuilderIndented();

			styleInclude.ForEach(c => builder.AppendLine(c));

			if (styleInclude.Count > 0)
				builder.AppendLine();

			builder
				.AppendLine($".{selector} {{")
				.AppendLine("}");

			WriteFile(path, file, builder);
		}

		private void Html(string path, string name, string selector, bool outlet)
		{
			var file = $"{selector}.html";

			if (File.Exists(Path.Combine(path, file)))
			{
				Console.WriteLine($"File already exists: {file}");
				return;
			}

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.AppendLine($"<div class=\"{selector}\">")
				.IncrementIndent()
				.AppendLine($"<h1>Hey! I\"m {selector}</h1>");


			if (outlet)
			{
				builder
					.AppendLine("<router-outlet></router-outlet>");
			}

			builder
				.DecrementIndent()
				.AppendLine("</div>");

			WriteFile(path, file, builder);
		}
	}
}
