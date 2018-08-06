using System.IO;
using System.Text;
using XCommon.Util;

namespace XCommon.CodeGenerator.Core.Implementation
{
	public class FileWriter : IFileWriter
	{
		public void WriteFile(string path, string file, StringBuilderIndented builder, bool overrideIfExists)
			=> WriteFile(path, file, builder.ToString(), overrideIfExists);

		public void WriteFile(string path, string file, StringBuilder builder, bool overrideIfExists)
			=> WriteFile(path, file, builder.ToString(), overrideIfExists);

		public void WriteFile(string path, string file, string content, bool overrideIfExists)
		{
			if (!overrideIfExists && File.Exists(Path.Combine(path, file)))
			{
				return;
			}

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			var fullPath = Path.Combine(path, file);
			File.WriteAllText(fullPath, content, Encoding.UTF8);
		}
	}
}
