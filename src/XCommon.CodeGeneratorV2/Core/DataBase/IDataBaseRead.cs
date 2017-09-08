using System.Collections.Generic;

namespace XCommon.CodeGeneratorV2.Core.DataBase
{
	public interface IDataBaseRead
    {
		IReadOnlyList<DataBaseSchema> Read();
    }
}
