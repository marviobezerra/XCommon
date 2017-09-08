using XCommon.CodeGeneratorV2.Core;
using XCommon.CodeGeneratorV2.Core.DataBase;

namespace XCommon.CodeGeneratorV2.CSharp
{
	public interface ICSharpEntityWriter
	{
		void WriteFilter(DataBaseTable item);

		void WriteEntity(DataBaseTable item);
    }
}
