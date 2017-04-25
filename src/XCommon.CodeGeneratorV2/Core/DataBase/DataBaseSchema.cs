using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.Core.DataBase
{
	public class DataBaseSchema
    {
		public DataBaseSchema(string name)
		{
			Name = name;
			Tables = new List<DataBaseTable>();
		}

		public string Name { get; private set; }

		public List<DataBaseTable> Tables { get; set; }
	}
}
