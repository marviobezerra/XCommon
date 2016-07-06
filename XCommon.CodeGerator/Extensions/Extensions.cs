using System;
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
    
	internal static class Extensions
	{
        internal static string GetSelector(this string component)
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