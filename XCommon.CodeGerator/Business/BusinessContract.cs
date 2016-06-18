using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommon.CodeGerator.Configuration;
using XCommon.CodeGerator.Entity;
using XCommon.CodeGerator.Extensions;
using XCommon.Util;

namespace XCommon.CodeGerator.Business
{
	internal class BusinessContract
    {
		private ConfigBusiness Config => Generator.Configuration.Business;

		private List<ItemGroup> ItemsGroup => Generator.ItemGroups;

		public void Run()
		{
			foreach (var group in ItemsGroup)
			{
				foreach (var item in group.Items)
				{
					string path = Path.Combine(Config.ContractPath, group.Name);
					string file = Path.Combine(path, $"I{item.Name}Business.cs");

					if (File.Exists(file))
						continue;

					var nameSpace = new List<string> { "System", "XCommon.Patterns.Repository" };
					nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}");
					nameSpace.Add($"{Config.EntrityNameSpace}.{group.Name}.Filter");

					StringBuilderIndented builder = new StringBuilderIndented();

					builder
						.InterfaceInit($"I{item.Name}Business", $"IRepository<{item.Name}Entity, {item.Name}Filter>", $"{Config.ContractNameSpace}.{group.Name}", ClassVisility.Public, nameSpace.ToArray())
						.InterfaceEnd();

					if (!Directory.Exists(path))
						Directory.CreateDirectory(path);

					File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
				}
			}
		}
	}
}
