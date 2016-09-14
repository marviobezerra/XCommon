using System.Collections.Generic;
using System.IO;
using XCommon.CodeGeratorV2.Core.Util;
using XCommon.CodeGeratorV2.CSharp.Configuration;
using XCommon.CodeGeratorV2.CSharp.Extensions;
using XCommon.Util;

namespace XCommon.CodeGeratorV2.CSharp.Writter
{
	internal class Contract : FileWriter
	{
		public void Run(CSharpConfig config)
		{
			foreach (var group in config.DataBaseItems)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(config.ContractPath, group.Name);
					string file = $"I{item.Name}Business.cs";

					if (File.Exists(Path.Combine(path, file)))
						continue;

					var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository" };
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}");
					nameSpace.Add($"{config.EntrityNameSpace}.{group.Name}.Filter");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.InterfaceInit($"I{item.Name}Business", $"IRepository<{item.Name}Entity, {item.Name}Filter>", $"{config.ContractNameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray())
						.InterfaceEnd();

					WriteFile(path, file, builder);
				}
			}
		}
	}
}
