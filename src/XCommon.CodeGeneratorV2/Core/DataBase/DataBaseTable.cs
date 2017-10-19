using System.Collections.Generic;
using System.Linq;

namespace XCommon.CodeGenerator.Core.DataBase
{
	public class DataBaseTable
    {
		public DataBaseTable(DataBaseSchema schema, string name)
		{
			Schema = schema.Name;
			Name = name;
			Columns = new List<DataBaseColumn>();
			RelationShips = new List<DataBaseRelationShip>();
		}

		public string Schema { get; private set; }

		public string Name { get; private set; }

		public string PKName
		{
			get
			{
				var pkColumn = Columns.FirstOrDefault(c => c.PK);
				return pkColumn == null ? string.Empty : pkColumn.Name;
			}
		}

		public List<DataBaseColumn> Columns { get; set; }

		public List<DataBaseRelationShip> RelationShips { get; set; }
	}
}
