using XCommon.CodeGeneratorV2.Core.DataBase;

namespace XCommon.CodeGeneratorV2.CSharp
{
	public interface ICSharpEnityFrameworkWriter
	{
		void WriteContext();

		void WriteEntity(DataBaseTable item);

		void WriteEntityMap(DataBaseTable item);
	}
}
