using System.Collections.Generic;
using System.IO;
using System.Linq;
using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;
using XCommon.CodeGenerator.Core.Extensions;
using XCommon.CodeGenerator.CSharp.Implementation.Helper;
using XCommon.Util;

namespace XCommon.CodeGenerator.CSharp.EntityFramework
{
	public class EntityFrameworkClassWriter : BaseWriter
	{
		public override bool Write()
		{
			foreach (var schema in Config.DataBaseItems)
			{
				foreach (var table in schema.Tables)
				{
					WriteEntity(table);
				}
			}

			return true;
		}

		protected virtual void WriteEntity(DataBaseTable item)
		{
			var path = Path.Combine(Config.CSharp.EntityFramework.Path, item.Schema);
			var file = $"{item.Name}.cs";

			var nameSpace = new List<string> { "System", "System.Collections.Generic" };
			nameSpace.AddRange(item.RelationShips.Where(c => c.TableFK != item.Schema).Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.SchemaFK}").Distinct());
			nameSpace.AddRange(item.RelationShips.Where(c => c.TablePK != item.Schema).Select(c => $"{Config.CSharp.EntityFramework.NameSpace}.{c.SchemaPK}").Distinct());
			nameSpace.AddRange(item.Columns.Where(c => c.Schema != item.Schema).Select(c => c.Schema));
			nameSpace.AddRange(item.ProcessRemapSchema(Config));


			var itemNameSpace = $"{Config.CSharp.EntityFramework.NameSpace}.{item.Schema}";
			nameSpace.RemoveAll(c => c == itemNameSpace);

			var builder = new StringBuilderIndented();

			builder
				.GenerateFileMessage()
				.ClassInit(item.Name, null, itemNameSpace, ClassVisibility.Public, nameSpace.ToArray());

			foreach (var property in item.Columns)
			{
				var propertyType = property.ProcessRemapProperty(Config);

				builder
					.AppendLine($"public {propertyType} {property.Name} {{ get; set; }}")
					.AppendLine();
			}

			foreach (var relationShip in item.RelationShips.Where(c => c.Type == DataBaseRelationShipType.Single))
			{
				var relationShipName = ProcessRelationShipName(relationShip, relationShip.TablePK);

				builder
					.AppendLine($"public virtual {relationShip.TablePK} {relationShipName} {{ get; set; }}")
					.AppendLine();
			}

			foreach (var relationShip in item.RelationShips.Where(c => c.Type == DataBaseRelationShipType.Many))
			{
				var relationShipName = ProcessRelationShipName(relationShip, relationShip.TableFK);

				builder
					.AppendLine($"public virtual ICollection<{relationShip.TableFK}> {relationShipName} {{ get; set; }}")
					.AppendLine();
			}

			builder
				.ClassEnd();

			Writer.WriteFile(path, file, builder, true);
		}

	}
}
