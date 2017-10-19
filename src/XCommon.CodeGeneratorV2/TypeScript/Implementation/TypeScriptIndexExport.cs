using System.IO;
using System.Linq;
using System.Text;
using XCommon.CodeGenerator.Core;

namespace XCommon.CodeGenerator.TypeScript.Implementation
{
	public class TypeScriptIndexExport : BaseWriter, ITypeScriptIndexExport
	{
		public void Run(string path)
		{
			Process(path);

			foreach (var dir in Directory.GetDirectories(path))
			{
				Process(dir);
			}
		}

		private void Process(string path)
		{
			if (!Directory.Exists(path))
			{
				return;
			}

			var file = $"index.ts";
			var builder = new StringBuilder();

			foreach (var fileName in Directory.GetFiles(path, "*.ts", SearchOption.TopDirectoryOnly).OrderBy(c => c))
			{
				if (Path.GetFileName(fileName) == "index.ts")
				{
					continue;
				}

				builder.AppendLine($"export * from './{CheckFileName(fileName)}';");
			}

			Writer.WriteFile(path, file, builder, true);
		}

		private string CheckFileName(string file)
		{
			var result = Path.GetFileName(file);
			result = result.Replace(".ts", string.Empty);
			return result;
		}
	}
}
