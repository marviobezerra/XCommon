using System.Linq;
using XCommon.Extensions.String;
using XCommon.Util;

namespace XCommon.CodeGenerator.Core.Extensions
{
	internal static class CSharpExtensions
	{
		internal static StringBuilderIndented ClassInit(this StringBuilderIndented builder, string className, string parent, string nameSpace, ClassVisibility visibily, params string[] usings)
		{
			return builder
				.ClassInit(className, parent, nameSpace, visibily, false, usings);
		}

		internal static StringBuilderIndented ClassInit(this StringBuilderIndented builder, string className, string parent, string nameSpace, ClassVisibility visibility, bool partial, params string[] usings)
		{
			return builder
				.AddUsing(usings)
				.AppendLine()
				.AppendLine($"namespace {nameSpace}")
				.AppendLine("{")
				.IncrementIndent()
				.Append(VisibilityParse(visibility))
				.Append(partial ? " partial" : string.Empty)
				.Append(" class ")
				.Append(className)
				.Append(parent.IsNotEmpty() ? $": {parent}" : string.Empty)
				.AppendLine()
				.AppendLine("{")
				.IncrementIndent();
		}

		internal static StringBuilderIndented AddUsing(this StringBuilderIndented builder, params string[] args)
		{
			foreach (var nameSpace in args.Distinct().OrderBy(c => c))
			{
				if (nameSpace.IsEmpty())
				{
					continue;
				}

				builder.AppendLine($"using {nameSpace};");
			}

			return builder;
		}

		internal static StringBuilderIndented InterfaceInit(this StringBuilderIndented builder, string interfaceName, string parent, string nameSpace, ClassVisibility visibily, params string[] usings)
		{
			return builder
				.InterfaceInit(interfaceName, parent, nameSpace, visibily, false, usings);
		}

		internal static StringBuilderIndented InterfaceInit(this StringBuilderIndented builder, string interfaceName, string parent, string nameSpace, ClassVisibility visibility, bool partial, params string[] usings)
		{
			return builder
				.AddUsing(usings)
				.AppendLine()
				.AppendLine($"namespace {nameSpace}")
				.AppendLine("{")
				.IncrementIndent()
				.Append(VisibilityParse(visibility))
				.Append(" interface ")
				.Append(interfaceName)
				.Append(parent.IsNotEmpty() ? $": {parent}" : string.Empty)
				.AppendLine()
				.AppendLine("{")
				.IncrementIndent();
		}

		internal static StringBuilderIndented InterfaceEnd(this StringBuilderIndented builder)
		{
			return ClassEnd(builder);
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

		internal static StringBuilderIndented GenerateFileMessage(this StringBuilderIndented builder)
		{
			builder.AppendLine("/***************************************** WARNING *****************************************/");
			builder.AppendLine("/* Don't write any code in this file, because it will be rewritten on the next generation. */");
			builder.AppendLine("/*******************************************************************************************/");
			builder.AppendLine();

			return builder;
		}

		private static string VisibilityParse(ClassVisibility visibility)
		{
			switch (visibility)
			{
				case ClassVisibility.PublicStatic:
					return "public static";
				case ClassVisibility.InternalStatic:
					return "internal static";
				case ClassVisibility.Private:
				case ClassVisibility.Public:
				case ClassVisibility.Internal:
				default:
					return visibility.ToString().ToLower();
			}
		}
	}
}
