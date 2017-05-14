using System;
using System.Data;
using System.Data.SqlClient;

namespace XCommon.Test.CodeGenerator.DataBase.Fixture
{
	public class DatabaseFixture : IDisposable
	{
		public string DBPath { get; set; }

		public string DBServer { get; set; }

		public void Dispose()
		{
			DetachMdf(DBServer, DBPath);
		}

		private void DetachMdf(string dbServer, string dbName)
		{
			SqlConnection.ClearAllPools();
			using (var conn = new SqlConnection(string.Format("Server={0};Database=master;Integrated Security=SSPI", dbServer)))
			{
				conn.Open();

				using (var cmd = new SqlCommand("sp_detach_db", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@dbname", dbName);
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}