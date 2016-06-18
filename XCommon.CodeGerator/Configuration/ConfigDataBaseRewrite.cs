using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCommon.CodeGerator.Configuration
{
	public class ConfigDataBaseRewrite
	{
		public string SchemaPK { get; set; }

		public string SchemaFK { get; set; }

		public string TablePK { get; set; }

		public string TableFK { get; set; }

		public string ColumnPK { get; set; }

		public string ColumnFK { get; set; }

		public string CustonName { get; set; }
	}
}
