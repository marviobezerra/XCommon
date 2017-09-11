using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;

namespace XCommon.CodeGenerator.CSharp
{
	public interface ICSharpRepositoryWriter
	{
		void WriteContract(DataBaseTable item);

		void WriteConcrete(DataBaseTable item);

		void WriteQuery(DataBaseTable item);

		void WriteValidation(DataBaseTable item);
	}
}
