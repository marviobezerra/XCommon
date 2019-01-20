using System.Collections.Generic;
using System.Linq;
using XCommon.CodeGenerator.Core.DataBase;

namespace XCommon.CodeGenerator.CSharp.Implementation.Helper
{
	public static class Remap
	{
		public static List<string> ProcessRemapSchema(this DataBaseTable item, GeneratorConfig config, bool isEF)
		{
			var result = new List<string>();

			if (config.CSharp.EntityFramework.Remap == null)
			{
				return result;
			}

			var remap = config.CSharp.EntityFramework.Remap.Where(
				c => c.Schema == item.Schema
				&& c.Table == item.Name
			);

			if (remap != null)
			{
				result.AddRange(remap.Select(c => c.NameSpace));
			}

			var applicationClass = config.CSharp.ApplicationClasses.Where(c => item.RelationShips.Any(x => x.SchemaPK == c.Schema && x.TablePK == c.Name));

			if (config.CSharp.UsingApplicationBase && applicationClass != null)
			{
				if (isEF)
				{
					result.AddRange(applicationClass.Select(c => c.NamespaceContext));
				}
				else
				{
					result.AddRange(applicationClass.Select(c => c.NamespaceEntity));

				}
			}

			return result;
		}

		public static string ProcessRemapProperty(this DataBaseColumn property, GeneratorConfig config)
		{
			var result = property.Type;

			if (config.CSharp.EntityFramework.Remap == null)
			{
				return result;
			}

			var remap = config.CSharp.EntityFramework.Remap.FirstOrDefault(
				c => c.Schema == property.Schema
				&& c.Table == property.Table
				&& c.Column == property.Name
			);

			if (remap != null)
			{
				result = remap.Type;
			}

			return result;
		}
	}
}
