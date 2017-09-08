using XCommon.CodeGeneratorV2.Core;
using XCommon.CodeGeneratorV2.Core.DataBase;

namespace XCommon.CodeGeneratorV2.CSharp
{
	public interface ICSharpRepositoryWriter
	{
		void WriteContract(DataBaseTable item);

		void WriteConcrete(DataBaseTable item);

		void WriteQuery(DataBaseTable item);

		void WriteValidation(DataBaseTable item);
	}
}
