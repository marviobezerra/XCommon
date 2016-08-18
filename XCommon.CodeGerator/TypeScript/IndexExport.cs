using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.CodeGerator.TypeScript
{
    public class IndexExport
    {
        private Configuration.ConfigAngular ConfigAngular => Generator.Configuration.Angular;
        private Configuration.ConfigEntity ConfigEntity => Generator.Configuration.Entity;


        public void Run()
        {
            Process(ConfigAngular.ServicePath);
            Process(ConfigAngular.DirectivePath);
            Process(ConfigAngular.ComponentPath);

            foreach (var dir in Directory.GetDirectories(ConfigAngular.ComponentPath))
            {
                Process(dir);
            }

            Process(ConfigEntity.Path);
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

                builder.AppendLine($"export * from \"./{Path.GetFileName(fileName)}\";");
            }

            File.WriteAllText(file, builder.ToString(), Encoding.UTF8);
        }
    }
}
