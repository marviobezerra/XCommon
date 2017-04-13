using System;
using System.Collections.Generic;
using System.IO;
using XCommon.CodeGenerator.Core.Util;
using XCommon.CodeGenerator.CSharp.Configuration;
using XCommon.CodeGenerator.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.Writter
{
	internal class Contract : FileWriter
	{
		public void Run(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					var path = Path.Combine(config.ContractPath, group.Name);
					var file = $"I{item.Name}Business.cs";

					if (File.Exists(Path.Combine(path, file)))
					{
						continue;
					}

					var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository" };
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");

					var builder = new StringBuilderIndented();

					builder
						.InterfaceInit($"I{item.Name}Business", $"IRepositoryEF<{item.Name}Entity, {item.Name}Filter>", $"{config.ContractNameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray())
						.InterfaceEnd();

					WriteFile(path, file, builder);
				}
			}

			Console.WriteLine("Generate contract code - OK");
		}
	}
}
