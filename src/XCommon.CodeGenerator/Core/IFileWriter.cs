using System.Text;
using XCommon.Util;

namespace XCommon.CodeGenerator.Core
{
	public interface IFileWriter
    {
		 void WriteFile(string path, string file, StringBuilderIndented builder, bool overrideIfExists);
		 void WriteFile(string path, string file, StringBuilder builder, bool overrideIfExists);
		 void WriteFile(string path, string file, string content, bool overrideIfExists);
	}
}
