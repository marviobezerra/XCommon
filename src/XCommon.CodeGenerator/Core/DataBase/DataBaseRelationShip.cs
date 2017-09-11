namespace XCommon.CodeGenerator.Core.DataBase
{
	public class DataBaseRelationShip
    {
		public string SchemaPK { get; set; }

		public string SchemaFK { get; set; }

		public string TablePK { get; set; }

		public string TableFK { get; set; }

		public string ColumnPK { get; set; }

		public string ColumnFK { get; set; }

		public DataBaseRelationShipType Type { get; set; }

		public bool Nullable { get; internal set; }
	}
}
