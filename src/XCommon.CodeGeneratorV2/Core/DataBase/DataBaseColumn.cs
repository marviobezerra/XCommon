using System;
using System.Collections.Generic;
using System.Text;

namespace XCommon.CodeGeneratorV2.Core.DataBase
{
    public class DataBaseColumn
    {
		public DataBaseColumn(string schema, string table)
		{
			Table = table;
			Schema = schema;
		}

		public string Schema { get; private set; }

		public string Table { get; private set; }

		public string Name { get; internal set; }

		public bool PK { get; set; }

		public bool Nullable { get; set; }

		public bool ColumnComputed { get; set; }

		public string Type { get; set; }

		public string TypeSql { get; set; }

		public string Description { get; set; }
	}
}
