using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using XCommon.CodeGenerator.Core.Entity;
using XCommon.CodeGenerator.CSharp.Configuration;

namespace XCommon.CodeGenerator.Core.DataBase
{
    internal class DataBaseRead
	{ 
		internal List<ItemGroup> ReadDataBase(string connectionString)
		{
			return ReadDataBase(new DataBaseConfig { ConnectionString = connectionString });
		}

		internal List<ItemGroup> ReadDataBase(DataBaseConfig config)
		{
			var result = new List<ItemGroup>();

			using (var cnx = new SqlConnection(config.ConnectionString))
			{
				cnx.Open();

				var dataBaseObjects = GetDataBaseObjects(config, cnx);

				foreach (var schemaItem in dataBaseObjects.Select(c => c.Schema).OrderBy(c => c).Distinct())
				{
					var schema = new ItemGroup
					{
						Name = schemaItem,
						Items = GetItems(config, cnx, dataBaseObjects, schemaItem)
					};

					result.Add(schema);
				}
			};

			return result;
		}

		private List<Item> GetItems(DataBaseConfig config, SqlConnection cnx, List<DataBaseObject> dataBaseObjects, string schema)
		{
			var result = new List<Item>();

			foreach (var tableItem in dataBaseObjects.Where(c => c.Schema == schema).Select(c => c.TableName).OrderBy(c => c).Distinct())
			{
				result.Add(new Item
				{
					Name = tableItem,
					Properties = GetProperties(config, dataBaseObjects, schema, tableItem),
					Relationships = GetRelationships(cnx, tableItem, schema)
				});
			}

			return result;
		}

		private List<ItemProperty> GetProperties(DataBaseConfig config, List<DataBaseObject> dataBaseObjects, string schema, string table)
		{
			var result = new List<ItemProperty>();

			foreach (var column in dataBaseObjects.Where(c => c.Schema == schema && c.TableName == table).OrderBy(c => c.ColumnOrder))
			{
				var remap = config.Remap.FirstOrDefault(c => c.Schema == schema && c.Table == table && c.Column == column.ColumnName);

				result.Add(new ItemProperty
				{
					Key = column.PK,
					NameGroup = remap != null ? remap.NameSpace : schema,
					Name = column.ColumnName,
					Nullable = column.Nullable,
					Size = column.Size,
					Type = remap != null ? remap.Type : column.Type,
					TypeSql = column.TypeSql
				});
			}

			return result;
		}

		private List<ItemRelationship> GetRelationships(SqlConnection cnx, string tabela, string schema)
		{
			var result = new List<ItemRelationship>();

			var cmd = new SqlCommand(SqlRelationship(tabela, schema), cnx);
			var reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				var relationShip = new ItemRelationship
				{
					ItemGroupPK = reader["SchemaPK"].ToString(),
					ItemPK = reader["TablePK"].ToString(),
					PropertyPK = reader["ColumnPK"].ToString(),

					ItemGroupFK = reader["SchemaFK"].ToString(),
					ItemFK = reader["TableFK"].ToString(),
					PropertyFK = reader["ColumnFK"].ToString(),

					RelationshipType = reader["Type"].ToString() == "M" ? ItemRelationshipType.Many : ItemRelationshipType.Single,
					Nullable = reader["Nullable"].ToString() == "1"
				};

				result.Add(relationShip);
			}

			return result;
		}

		private List<DataBaseObject> GetDataBaseObjects(DataBaseConfig config, SqlConnection cnx)
		{
			var result = new List<DataBaseObject>();

			var cmd = new SqlCommand(SqlDataObjects(config), cnx);
			var data = cmd.ExecuteReader();

			while (data.Read())
			{
				result.Add(new DataBaseObject
				{
					Nullable = data["Nullable"].ToString().ToUpper() == "TRUE",
					ColumnComputed = data["ColumnComputed"].ToString().ToUpper() == "TRUE",
					ColumnName = data["ColumnName"].ToString(),
					Description = data["Description"].ToString(),
					IsTable = data["IsTable"].ToString() == "1",
					ColumnOrder = int.Parse(data["ColumnOrder"].ToString()),
					PK = data["PK"].ToString() == "1",
					Schema = data["Schema"].ToString(),
					TableName = data["TableName"].ToString(),
					Type = GetCSharpType(data["Type"].ToString(), data["Nullable"].ToString().ToUpper() == "TRUE"),
					TypeSql = data["Type"].ToString(),
					Size = data["Size"].ToString(),
					Scale = data["Scale"].ToString()
				});
			}

			return result;
		}

		private string SqlDataObjects(DataBaseConfig config)
		{
			var sql =
				"SELECT\n" +
				"    X.IsTable,\n" +
				"    X.[Schema],\n" +
				"    X.TableName,\n" +
				"    X.ColumnName,\n" +
				"    X.ColumnOrder,\n" +
				"    X.PK,\n" +
				"    X.Nullable,\n" +
				"    X.Type,\n" +
				"    X.Size,\n" +
				"    X.Scale,\n" +
				"    X.ColumnComputed,\n" +
				"    CASE\n" +
				"       WHEN X.PK = 1 THEN LTRIM(CAST(ISNULL(X.Description, '') AS VARCHAR) + ' Key field to table [' + X.[SCHEMA] + '].[' + X.TableName + ']')\n" +
				"       WHEN X.Size IS NOT NULL AND X.Size <> 'MAX' THEN LTRIM(CAST(ISNULL(X.Description, '') AS VARCHAR) + ' Max size ' + CAST(X.Size AS VARCHAR))\n" +
				"       WHEN X.Size IS NOT NULL AND X.Size = 'MAX' THEN LTRIM(CAST(ISNULL(X.Description, '') AS VARCHAR) + ' Free text')\n" +
				"       ELSE X.Description\n" +
				"    END AS Description\n" +
				"FROM (	SELECT\n" +
				"            1 AS IsTable,\n" +
				"            SCHEMA_NAME(TB.[SCHEMA_ID]) AS [Schema],\n" +
				"            TB.NAME AS TableName,\n" +
				"            CL.NAME AS ColumnName,\n" +
				"            CL.column_id AS ColumnOrder,\n" +
				"            (SELECT\n" +
				"                CASE WHEN C.name = CL.name THEN 1\n" +
				"                     ELSE 0\n" +
				"                END\n" +
				"             FROM\n" +
				"                SYS.INDEXES I\n" +
				"                JOIN SYS.INDEX_COLUMNS IC ON IC.INDEX_ID = I.INDEX_ID AND IC.[OBJECT_ID] = I.[OBJECT_ID]\n" +
				"                JOIN SYS.[COLUMNS] C ON C.COLUMN_ID = IC.COLUMN_ID AND C.[OBJECT_ID] = IC.[OBJECT_ID]\n" +
				"             WHERE\n" +
				"                I.IS_PRIMARY_KEY = 1\n" +
				"                AND I.[OBJECT_ID] = TB.object_id) AS PK,\n" +
				"            CL.IS_NULLABLE AS Nullable,\n" +
				"            TP.NAME Type,\n" +
				"            CASE WHEN TP.NAME IN ('VARCHAR', 'CHAR') AND CL.MAX_LENGTH > 0 THEN CAST(CL.MAX_LENGTH AS VARCHAR)\n" +
				"                 WHEN TP.NAME IN ('NVARCHAR', 'NCHAR') AND CL.MAX_LENGTH > 0 THEN CAST(CL.MAX_LENGTH / 2 AS VARCHAR)\n" +
				"                 WHEN TP.NAME IN ('VARCHAR', 'CHAR') AND CL.MAX_LENGTH = -1 THEN 'MAX'\n" +
				"                 WHEN TP.NAME IN ('NVARCHAR', 'NCHAR') AND CL.MAX_LENGTH = -1 THEN 'MAX'\n" +
				"                 WHEN TP.NAME IN ('DECIMAL', 'NUMERIC') THEN CAST(CL.precision AS VARCHAR(MAX))\n" +
				"                 ELSE NULL\n" +
				"            END AS Size,\n" +
				"            CASE\n" +
				"					WHEN TP.NAME IN ('DECIMAL', 'NUMERIC') THEN CAST(CL.scale AS VARCHAR(MAX))\n" +
				"					ELSE NULL\n" +
				"            END AS Scale,\n" +
				"            CL.IS_COMPUTED AS ColumnComputed,\n" +
				"            P.[VALUE] AS Description\n" +
				"        FROM\n" +
				"            SYS.TABLES TB\n" +
				"            JOIN SYS.[COLUMNS] CL ON CL.[OBJECT_ID] = TB.[OBJECT_ID]\n" +
				"            JOIN SYS.TYPES TP ON TP.SYSTEM_TYPE_ID = CL.SYSTEM_TYPE_ID\n" +
				"            LEFT JOIN SYS.EXTENDED_PROPERTIES AS P ON P.MAJOR_ID = TB.OBJECT_ID AND P.MINOR_ID = CL.COLUMN_ID\n" +
				"							                          AND P.CLASS = 1 AND P.NAME = 'MS_DESCRIPTION'\n" +
				"        UNION ALL\n" +
				"        SELECT\n" +
				"            0 AS IsTable,\n" +
				"            SCHEMA_NAME(TB.[SCHEMA_ID]) AS [SCHEMA],\n" +
				"            TB.NAME AS TableName,\n" +
				"            CL.NAME AS ColumnName,\n" +
				"            CL.column_id AS ColumnOrder,\n" +
				"            0 AS PK,\n" +
				"            CL.IS_NULLABLE AS Nullable,\n" +
				"            TP.NAME Type,\n" +
				"            CASE WHEN TP.NAME IN ('VARCHAR', 'CHAR') AND CL.MAX_LENGTH > 0 THEN CAST(CL.MAX_LENGTH AS VARCHAR)\n" +
				"                 WHEN TP.NAME IN ('NVARCHAR', 'NCHAR') AND CL.MAX_LENGTH > 0 THEN CAST(CL.MAX_LENGTH / 2 AS VARCHAR)\n" +
				"                 WHEN TP.NAME IN ('VARCHAR', 'CHAR') AND CL.MAX_LENGTH = -1 THEN 'MAX'\n" +
				"                 WHEN TP.NAME IN ('NVARCHAR', 'NCHAR') AND CL.MAX_LENGTH = -1 THEN 'MAX'\n" +
				"                 WHEN TP.NAME IN ('DECIMAL', 'NUMERIC') THEN CAST(CL.precision AS VARCHAR(MAX))\n" +
				"                 ELSE NULL\n" +
				"            END AS Size,\n" +
				"            CASE\n" +
				"					WHEN TP.NAME IN ('DECIMAL', 'NUMERIC') THEN CAST(CL.scale AS VARCHAR(MAX))\n" +
				"					ELSE NULL\n" +
				"            END AS Scale,\n" +
				"            CL.IS_COMPUTED AS ColumnComputed,\n" +
				"            P.[VALUE] AS Description\n" +
				"        FROM\n" +
				"            SYS.VIEWS TB\n" +
				"            JOIN SYS.[COLUMNS] CL ON CL.[OBJECT_ID] = TB.[OBJECT_ID]\n" +
				"            JOIN SYS.TYPES TP ON TP.SYSTEM_TYPE_ID = CL.SYSTEM_TYPE_ID\n" +
				"            LEFT JOIN SYS.EXTENDED_PROPERTIES AS P ON P.MAJOR_ID = TB.OBJECT_ID\n" +
				"							                          AND P.MINOR_ID = CL.COLUMN_ID\n" +
				"							                          AND P.CLASS = 1\n" +
				"							                          AND P.NAME = 'MS_DESCRIPTION'\n" +
				"        ) X\n" +
				"		WHERE\n" +
				"			X.Type NOT IN ('hierarchyid', 'geometry')\n";

			if (config.SchemaExclude.Count > 0)
			{
				var schemas = string.Join(", ", config.SchemaExclude);
				sql += $"			AND X.[Schema] NOT IN ('{schemas}')\n";
			}

			if (config.TableExclude.Count > 0)
			{
				var tables = string.Join(", ", config.TableExclude);
				sql += $"			AND X.TableName NOT IN ('{tables}')\n";
			}

			sql += 
				" ORDER BY\n" +
				"			1, 2, 3, 5\n";

			return sql;
		}

		private string SqlRelationship(string table, string schema)
		{
			var sql = "DECLARE \n"
				+ " @Table VARCHAR(100) = '" + schema + "." + table + "' \n"
				+ " \n"
				+ " SELECT \n"
				+ "     SCHEMA_NAME(TBO.SCHEMA_ID) AS SchemaPK, \n"
				+ "     TBO.Name AS TablePK, \n"
				+ "     CLO.Name AS ColumnPK, \n"
				+ "     SCHEMA_NAME(TBD.SCHEMA_ID) AS SchemaFK, \n"
				+ "     TBD.Name AS TableFK, \n"
				+ "     CLD.Name AS ColumnFK, \n"
				+ "     CLD.is_nullable AS Nullable, \n"
				+ "     'M' AS Type \n"
				+ " FROM \n"
				+ "     SYS.TABLES AS TBO \n"
				+ "     JOIN SYS.FOREIGN_KEY_COLUMNS FKC ON FKC.REFERENCED_OBJECT_ID = TBO.[OBJECT_ID] \n"
				+ "     JOIN SYS.TABLES AS TBD ON TBD.OBJECT_ID = FKC.PARENT_OBJECT_ID \n"
				+ "     JOIN SYS.COLUMNS AS CLD ON CLD.COLUMN_ID = FKC.PARENT_COLUMN_ID AND CLD.OBJECT_ID = FKC.PARENT_OBJECT_ID \n"
				+ "     JOIN SYS.COLUMNS AS CLO ON CLO.COLUMN_ID = FKC.REFERENCED_COLUMN_ID AND CLO.OBJECT_ID = FKC.REFERENCED_OBJECT_ID \n"
				+ " WHERE \n"
				+ "     TBO.OBJECT_ID = OBJECT_ID(@Table) \n"
				+ "     AND CLD.COLUMN_ID <> CLO.COLUMN_ID \n"
				+ "     AND TBO.OBJECT_ID <> TBD.OBJECT_ID\n"
				+ "  \n"
				+ " UNION ALL \n"
				+ "  \n"
				+ " SELECT \n"
				+ "     SCHEMA_NAME(TBD.SCHEMA_ID) AS SchemaPK, \n"
				+ "     TBD.Name AS TablePK, \n"
				+ "     CLD.Name AS ColumnPK, \n"
				+ "     SCHEMA_NAME(TBO.SCHEMA_ID) AS SchemaFK, \n"
				+ "     TBO.Name AS TableFK, \n"
				+ "     CLO.Name AS ColumnFK, \n"
				+ "     CLO.is_nullable AS Nullable, \n"
				+ "     'U' AS Type \n"
				+ " FROM \n"
				+ "     SYS.TABLES AS TBO \n"
				+ "     JOIN SYS.FOREIGN_KEY_COLUMNS FKC ON FKC.PARENT_OBJECT_ID = TBO.[OBJECT_ID] \n"
				+ "     JOIN SYS.TABLES AS TBD ON TBD.OBJECT_ID = FKC.REFERENCED_OBJECT_ID \n"
				+ "     JOIN SYS.COLUMNS AS CLD ON CLD.COLUMN_ID = FKC.REFERENCED_COLUMN_ID AND CLD.OBJECT_ID = FKC.REFERENCED_OBJECT_ID \n"
				+ "     JOIN SYS.COLUMNS AS CLO ON CLO.COLUMN_ID = FKC.PARENT_COLUMN_ID AND CLO.OBJECT_ID = FKC.PARENT_OBJECT_ID \n"
				+ " WHERE \n"
				+ "     TBO.OBJECT_ID = OBJECT_ID(@Table) \n"
				+ "     AND TBO.OBJECT_ID <> TBD.OBJECT_ID \n";

			return sql;
		}

		private string GetCSharpType(string sqlType, bool nullable)
		{
			var result = "string";

			switch (sqlType)
			{
				case "bigint":
					result = "long";
					break;
				case "smallint":
					result = "short";
					break;
				case "int":
					result = "int";
					break;
				case "uniqueidentifier":
					result = "Guid";
					break;
				case "smalldatetime":
				case "datetime2":
				case "datetime":
				case "date":
					result = "DateTime";
					break;
				case "float":
					result = "double";
					break;
				case "real":
				case "numeric":
				case "smallmoney":
				case "decimal":
				case "money":
					result = "decimal";
					break;
				case "tinyint":
					result = "byte";
					break;
				case "bit":
					result = "bool";
					break;
				case "image":
				case "binary":
				case "varbinary":
				case "timestamp":
					result = "byte[]";
					break;
				case "geography":
					result = "DbGeography";
					break;
			}

			return result + GetCSharpTypeNullable(nullable, sqlType);
		}

		private string GetCSharpTypeNullable(bool nullable, string sqlType)
		{
			var Retorno = nullable ? "?" : "";

			switch (sqlType)
			{
				case "image":
				case "binary":
				case "varbinary":
				case "timestamp":
				case "varchar":
				case "nvarchar":
				case "char":
				case "nchar":
				case "geography":
					Retorno = "";
					break;
			}

			return Retorno;
		}
	}
}
