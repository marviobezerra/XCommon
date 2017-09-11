using XCommon.CodeGenerator.Core;
using XCommon.CodeGenerator.Core.DataBase;

namespace XCommon.CodeGenerator.CSharp
{
	public interface ICSharpEntityWriter
	{
		void WriteFilter(DataBaseTable item);

		void WriteEntity(DataBaseTable item);
    }
}
