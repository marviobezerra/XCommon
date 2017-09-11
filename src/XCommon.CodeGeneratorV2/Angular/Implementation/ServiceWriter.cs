using System.Collections.Generic;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGeneratorV2.Angular.Implementation
{
	public class ServiceWriter : BaseWriter, IServiceWriter
	{
		public void Run(string path, List<string> services)
		{
			path = path.ToLower();

			foreach (var service in services)
			{
				var file = $"{service.GetSelector()}.service.ts".ToLower();
				var builder = new StringBuilderIndented();

				builder
					.AppendLine("import { Injectable } from '@angular/core'; ")
					.AppendLine("import { Http, Response } from '@angular/http'; ")
					.AppendLine()
					.AppendLine("@Injectable()")
					.AppendLine($"export class {service.GetName()}Service {{")
					.IncrementIndent()
					.AppendLine("constructor(private http: Http) { }")
					.DecrementIndent()
					.AppendLine("}");

				Writer.WriteFile(path, file, builder, false);
			}
		}
	}
}
