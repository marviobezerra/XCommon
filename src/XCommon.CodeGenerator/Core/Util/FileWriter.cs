using System.IO;
using System.Text;
using XCommon.Util;

namespace XCommon.CodeGenerator.Core.Util
{
	public abstract class FileWriter
    {
		protected void WriteFile(string path, string file, StringBuilderIndented builder)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			string fullPath = Path.Combine(path, file);

			File.WriteAllText(fullPath, builder.ToString(), Encoding.UTF8);
		}
	}
}
