using System;
using System.Text;
using XCommon.Util;

namespace XCommon.CodeGenerator.Core.Implementation
{
	public class InMemoryFileWriter : IFileWriter
	{
		public void WriteFile(string path, string file, StringBuilderIndented builder, bool overrideIfExists)
			=> WriteFile(path, file, builder.ToString(), overrideIfExists);

		public void WriteFile(string path, string file, StringBuilder builder, bool overrideIfExists)
			=> WriteFile(path, file, builder.ToString(), overrideIfExists);

		public void WriteFile(string path, string file, string content, bool overrideIfExists)
		{
			throw new NotImplementedException();
		}
	}
}
