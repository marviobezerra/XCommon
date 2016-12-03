using System.IO;
using System.Linq;
using System.Text;

namespace XCommon.CodeGenerator.Angular.Writter
{
	public class IndexExport
    {
        public void Run(string rootDir)
        {
			Process(rootDir);

            foreach (var dir in Directory.GetDirectories(rootDir))
            {
                Process(dir);
            }
        }

        private void Process(string dir)
        {
            if (!Directory.Exists(dir))
                return;

            string file = $"{dir}\\index.ts";
            StringBuilder builder = new StringBuilder();

            foreach (var fileName in Directory.GetFiles(dir, "*.ts", SearchOption.TopDirectoryOnly).OrderBy(c => c))
            {
                if (Path.GetFileName(fileName) == "index.ts")
                    continue;

                builder.AppendLine($"export * from \"./{CheckFileName(fileName)}\";");
            }

            File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
        }

		private string CheckFileName(string file)
		{
			var result = Path.GetFileName(file);
			result = result.Replace(".ts", string.Empty);
			return result;
		}
    }
}
