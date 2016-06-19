using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XCommon.Extensions.String;
using XCommon.Util;
using System.Linq;

namespace XCommon.CodeGerator.Angular2
{
	internal class Component
    {
		private Configuration.ConfigAngular Config => Generator.Configuration.Angular;

		internal void Run(string feature, List<string> components)
		{
			string path = Path.Combine(Config.ComponentPath, feature);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			foreach (string component in components)
			{
				var selector = GetSelector(component);

				TypeScript(path, component, selector);
				Sass(path, component, selector);
				Html(path, component, selector);
			}

			UpdateSassReference();
		}

		private string GetSelector(string component)
		{
			var result = string.Empty;

			foreach (var item in component)
			{
				if (result.IsEmpty())
				{
					result += item;
					continue;
				}

				if (Char.IsUpper(item))
				{
					result += '-';
					result += item;
					continue;
				}

				result += item;
			}

			return result.ToLower();
		}
		
		private void TypeScript(string path, string name, string selector)
		{
			var file = Path.Combine(path, $"{selector}.component.ts");

			if (File.Exists(file))
			{
				Console.WriteLine($"File already exists: {file}");
				return;
			}

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.AppendLine("import { Component, OnInit } from '@angular/core';")
				.AppendLine()
				.AppendLine("@Component({")
				.IncrementIndent()
				.AppendLine($"selector: '{selector}',")
				.AppendLine($"templateUrl: \"{selector}.component.html\"")
				.DecrementIndent()
				.AppendLine("})")
				.AppendLine($"export class {name}Component implements OnInit {{")
				.IncrementIndent()
				.AppendLine("constructor() { }")
				.AppendLine()
				.AppendLine("ngOnInit() { }")
				.DecrementIndent()
				.AppendLine("}");

			File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
		}

		private void Sass(string path, string name, string selector)
		{
			var file = Path.Combine(path, $"{selector}.scss");

			if (File.Exists(file))
			{
				Console.WriteLine($"File already exists: {file}");
				return;
			}

			StringBuilderIndented builder = new StringBuilderIndented();

			Config.StyleInclude.ForEach(c => builder.AppendLine(c));

			if (Config.StyleInclude.Count > 0)
				builder.AppendLine();

			builder
				.AppendLine($".{selector} {{")
				.AppendLine("}");

			File.WriteAllText(file, builder.ToString());
		}

		private void Html(string path, string name, string selector)
		{
			var file = Path.Combine(path, $"{selector}.html");

			if (File.Exists(file))
			{
				Console.WriteLine($"File already exists: {file}");
				return;
			}

			StringBuilderIndented builder = new StringBuilderIndented();

			builder
				.AppendLine($"<h1>Hey! I'm {selector}</h1>");

			File.WriteAllText(file, builder.ToString());
		}

		private void UpdateSassReference()
		{
			var path = Config.StylePath;
			var file = Path.Combine(path, Config.StyleMain);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			StringBuilder content = new StringBuilder();
			List<string> files = new List<string>();
			files.AddRange(GetFiles(file, path));
			files.AddRange(GetFiles(file, Config.AppRoot));
			files.AddRange(Config.StyleMainExtra);

			foreach (var item in files.Distinct().OrderBy(c => c))
			{
				var text = item
					.Replace(path, string.Empty)
					.Replace(@"\", @"/")
					.Replace(".scss", string.Empty);

				content.AppendLine($"@import \"{text}\";");
			}

			File.WriteAllText(file, content.ToString());
		}

		private static List<string> GetFiles(string fileBase, params string[] paths)
		{
			List<string> result = new List<string>();

			foreach (var path in paths)
			{
				foreach (var file in Directory.GetFiles(path, "*.scss", SearchOption.AllDirectories))
				{
					Uri uri1 = new Uri(fileBase);
					Uri uri2 = new Uri(file);
					var filePath = uri1.MakeRelativeUri(uri2).ToString();

					if (!filePath.IsEmpty())
						result.Add(filePath);
				}
			}

			return result;
		}
	}
}
