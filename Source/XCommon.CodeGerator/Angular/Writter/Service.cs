using System;
using System.Collections.Generic;
using System.IO;
using XCommon.CodeGerator.Angular.Extensions;
using XCommon.CodeGerator.Core.Util;
using XCommon.Util;

namespace XCommon.CodeGerator.Angular.Writter
{
	internal class Service : FileWriter
	{
        internal void Run(string path, List<string> services)
		{
			path = path.ToLower();

			foreach (string service in services)
			{
				var file = Path.Combine(path, $"{service.GetSelector()}.service.ts").ToLower();

				if (File.Exists(Path.Combine(path, file)))
				{
					Console.WriteLine($"File already exists: {file}");
					return;
				}

				StringBuilderIndented builder = new StringBuilderIndented();

				builder
					.AppendLine("import { Injectable } from \"@angular/core\"; ")
					.AppendLine("import { Http, Response } from \"@angular/http\"; ")
					.AppendLine()
					.AppendLine("@Injectable()")
					.AppendLine($"export class {service.GetName()}Service {{")
					.IncrementIndent()
					.AppendLine("constructor(private http: Http) { }")
					.DecrementIndent()
					.AppendLine("}");

				WriteFile(path, file, builder);

				Console.WriteLine($"Generated component {service.GetName()}");
			}
		}
	}
}
