using System.Linq;
using System.Text.RegularExpressions;
using XCommon.Extensions.String;
using XCommon.Util;

namespace XCommon.CodeGerator.Extensions
{
	internal enum ClassVisility
	{
		Private,
		Public,
		Internal
	}

	internal enum MethodVisility
	{
		Private,
		Public,
		Internal,
		Protected
	}

	internal static class Extensions
	{
		//internal static string ConfigBasePath(string file)
		//{
		//	var appSettings = string.IsNullOrEmpty(file)
		//			? Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "appsettings.json")
		//			: file;

		//	return Path.GetDirectoryName(appSettings);
		//}

		//internal static IConfigurationBuilder ConfigLoad(string file)
		//{
		//	var appSettings = string.IsNullOrEmpty(file)
		//			? Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "appsettings.json")
		//			: file;

		//	var appDevSettings = appSettings
		//			.Replace("appsettings.json", "appsettings.development.json");

		//	var basePath = Path.GetDirectoryName(appSettings);

		//	return new ConfigurationBuilder()
		//		.AddJsonFile(appSettings)
		//		.AddJsonFile(appDevSettings, true)
		//		.AddEnvironmentVariables();
		//}

		internal static string SafeName(this string name)
		{
			return name.Replace("-", string.Empty)
				.Replace(".", string.Empty);
		}

		internal static string Replace(this string str, string from, string to, bool ignoreCase)
		{
			str = Regex.Replace(str, from, to, RegexOptions.IgnoreCase);
			return str;
		}

		internal static StringBuilderIndented AddUsing(this StringBuilderIndented builder, params string[] args)
		{
			foreach (var nameSpace in args.Distinct().OrderBy(c => c))
			{
				if (nameSpace.IsEmpty())
					continue;

				builder.AppendLine($"using {nameSpace};");
			}

			return builder;
		}

		internal static StringBuilderIndented GenerateFileMessage(this StringBuilderIndented builder)
		{
			builder.AppendLine("/***************************************** WARNING *****************************************/");
			builder.AppendLine("/* Don't write any code in this file, because it will be rewritten on the next generation. */");
			builder.AppendLine("/*******************************************************************************************/");
			builder.AppendLine();

			return builder;
		}

		internal static StringBuilderIndented InterfaceInit(this StringBuilderIndented builder, string interfaceName, string parent, string nameSpace, ClassVisility visibily, params string[] usings)
		{
			return builder
				.InterfaceInit(interfaceName, parent, nameSpace, visibily, false, usings);
		}

		internal static StringBuilderIndented InterfaceInit(this StringBuilderIndented builder, string interfaceName, string parent, string nameSpace, ClassVisility visibily, bool partial, params string[] usings)
		{
			return builder
				.AddUsing(usings)
				.AppendLine()
				.AppendLine($"namespace {nameSpace}")
				.AppendLine("{")
				.IncrementIndent()
				.Append(visibily.ToString().ToLower())
				.Append(" interface ")
				.Append(interfaceName)
				.Append(parent.IsNotEmpty() ? $": {parent}" : string.Empty)
				.AppendLine()
				.AppendLine("{")
				.IncrementIndent();
		}

		internal static StringBuilderIndented InterfaceEnd(this StringBuilderIndented builder)
		{
			builder
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent()
				.AppendLine("}");

			return builder;
		}

		internal static StringBuilderIndented ClassInit(this StringBuilderIndented builder, string className, string parent, string nameSpace, ClassVisility visibily, params string[] usings)
		{
			return builder
				.ClassInit(className, parent, nameSpace, visibily, false, usings);
		}

		internal static StringBuilderIndented ClassInit(this StringBuilderIndented builder, string className, string parent, string nameSpace, ClassVisility visibily, bool partial, params string[] usings)
		{
			return builder
				.AddUsing(usings)
				.AppendLine()
				.AppendLine($"namespace {nameSpace}")
				.AppendLine("{")
				.IncrementIndent()
				.Append(visibily.ToString().ToLower())
				.Append(partial ? " partial" : string.Empty)
				.Append(" class ")
				.Append(className)
				.Append(parent.IsNotEmpty() ? $": {parent}" : string.Empty)
				.AppendLine()
				.AppendLine("{")
				.IncrementIndent();
		}

		internal static StringBuilderIndented ClassEnd(this StringBuilderIndented builder)
		{
			builder
				.DecrementIndent()
				.AppendLine("}")
				.DecrementIndent()
				.AppendLine("}");

			return builder;
		}
	}
}