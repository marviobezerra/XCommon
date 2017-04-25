using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.Core.DataBase
{
	public class DataBaseTable
    {
		public DataBaseTable(string schema)
		{
			Schema = schema;
			Columns = new List<DataBaseColumn>();
			RelationShips = new List<DataBaseRelationShip>();
		}

		public string Schema { get; private set; }

		public string Name { get; set; }

		public List<DataBaseColumn> Columns { get; set; }

		public List<DataBaseRelationShip> RelationShips { get; set; }
	}
}
