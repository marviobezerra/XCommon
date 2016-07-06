using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XCommon.CodeGerator.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.Angular2
{
    internal class Service
	{
		private Configuration.ConfigAngular Config => Generator.Configuration.Angular;
        
        internal void Run(List<string> services)
		{
			string path = Path.Combine(Config.ServicePath);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			foreach (string service in services)
			{
				var file = Path.Combine(path, $"{service.GetSelector()}.service.ts").ToLower();

				if (File.Exists(file))
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
					.AppendLine($"export class {service}Service {{")
					.IncrementIndent()
					.AppendLine("constructor(private http: Http) { }")
					.DecrementIndent()
					.AppendLine("}");
				
				File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
			}
		}
	}
}
