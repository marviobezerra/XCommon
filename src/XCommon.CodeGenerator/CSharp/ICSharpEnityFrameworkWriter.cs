using XCommon.CodeGenerator.Core.DataBase;

namespace XCommon.CodeGenerator.CSharp
{
	public interface ICSharpEnityFrameworkWriter
	{
		void WriteContext();

		void WriteEntity(DataBaseTable item);

		void WriteEntityMap(DataBaseTable item);
	}
}
