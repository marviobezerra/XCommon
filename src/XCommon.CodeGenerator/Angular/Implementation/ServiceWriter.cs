using System.Collections.Generic;
using XCommon.CodeGenerator.Angular.Configuration;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.Angular.Implementation
{
	public class ServiceWriter : BaseWriter, IServiceWriter
	{
		private string Quote
		{
			get
			{
				return Config.Angular.QuoteType == QuoteType.Double
					? "\""
					: "'";
			}
		}

		public void Run(string path, List<string> services)
		{
			path = path.ToLower();

			foreach (var service in services)
			{
				var file = $"{service.GetSelector()}.service.ts".ToLower();
				var builder = new StringBuilderIndented();

				builder
					.AppendLine($"import {{ Injectable }} from {Quote}@angular/core{Quote}; ")
					.AppendLine("import { Http, Response } from {Quote}@angular/http{Quote}; ")
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
