using System;
using System.Collections.Generic;
using System.Text;

namespace XCommon.CodeGenerator.Core.DataBase
{
    public class DataBaseColumn
    {
		public DataBaseColumn(DataBaseSchema schema, DataBaseTable table, string name)
		{
			Name = name;
			Table = table.Name;
			Schema = schema.Name;
		}

		public string Schema { get; private set; }

		public string Table { get; private set; }

		public string Name { get; private set; }

		public bool PK { get; set; }

		public bool Nullable { get; set; }

		public bool ColumnComputed { get; set; }

		public string Type { get; set; }

		public string Size { get; set; }

		public string TypeSql { get; set; }

		public string Description { get; set; }
	}
}
