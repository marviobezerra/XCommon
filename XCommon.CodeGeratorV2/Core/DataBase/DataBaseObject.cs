namespace XCommon.CodeGeratorV2.Core.DataBase
{
	internal class DataBaseObject
	{
		public bool IsTable { get; set; }

		public string Schema { get; set; }

		public string TableName { get; set; }

		public string ColumnName { get; set; }

		public int ColumnOrder { get; set; }

		public bool PK { get; set; }

		public bool Nullable { get; set; }

		public string Type { get; set; }

		public string TypeSql { get; set; }

		public string Size { get; set; }

		public string Scale { get; set; }

		public bool ColumnComputed { get; set; }

		public string Description { get; set; }
	}
}
