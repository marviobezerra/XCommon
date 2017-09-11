using System.Collections.Generic;

namespace XCommon.CodeGenerator.Core.DataBase
{
	public interface IDataBaseRead
    {
		IReadOnlyList<DataBaseSchema> Read();
    }
}
