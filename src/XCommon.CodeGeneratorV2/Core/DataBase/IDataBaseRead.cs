using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.Core.DataBase
{
	public interface IDataBaseRead
    {
		List<DataBaseSchema> Read(string connectionString);
    }
}
