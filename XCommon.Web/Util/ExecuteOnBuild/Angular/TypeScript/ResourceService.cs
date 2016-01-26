using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using XCommon.Util;

namespace XCommon.Web.Util.ExecuteOnBuild.Angular.TypeScript
{
    public class ResourceInfo
    {
        public ResourceInfo()
        {
            Cultures = new List<string>();
            Details = new List<ResourceDetails>();
        }

        public List<string> Cultures { get; set; }

        public string CultureDefault { get; set; }

        public List<ResourceDetails> Details { get; set; }
    }

    public class ResourceDetails
    {
        public Type Resource { get; set; }

        public ResourceManager Manager { get; set; }
    }

    internal class JSHelper
    {
        public string Item { get; set; }

        public string Valor { get; set; }
    }


    public abstract class ResourceService : IExecuteOnBuild
    {
        public ResourceService(string module, string registerScript)
        {
            Info = GetInfo();
            Module = module;
            RegisterScript = registerScript;
        }

        private ResourceInfo Info { get; set; }

        private string Module { get; set; }

        private string RegisterScript { get; set; }

        protected abstract ResourceInfo GetInfo();

        protected virtual string GetFileName(string basePath)
        {
            var dir = Path.Combine(basePath, "Scripts", "Common");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Path.Combine(dir, "ResourceService.ts");
        }

        public void Execute(string assemblyPath)
        {
            var dir = Directory.GetParent(Path.GetDirectoryName(assemblyPath)).FullName;
            var file = GetFileName(dir);
            File.WriteAllText(file, Build(), Encoding.UTF8);
        }

        private string Build()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(string.Format("module {0} {{", Module));
            result.AppendLine("");

            result.Append(BuildLanguageSuported());
            result.Append(BuildInterface());
            result.Append(BuildClass());
            result.Append(BuidService());

            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine(RegisterScript);

            return result.ToString();
        }

        private List<JSHelper> GetResouces(ResourceDetails info, string culture)
        {
            List<JSHelper> retorno = new List<JSHelper>();

            foreach (var item in info.Resource.GetProperties().Where(c => c.PropertyType == typeof(string)))
            {
                retorno.Add(new JSHelper
                {
                    Item = item.Name,
                    Valor = info.Manager.GetString(item.Name, new CultureInfo(culture))
                });
            }

            return retorno;
        }

        private string BuildInterface()
        {
            StringBuilder result = new StringBuilder();

            foreach (var detail in Info.Details)
            {
                result.AppendLine(string.Format("\tinterface I{0} {{", detail.Resource.Name));

                foreach (var resource in GetResouces(detail, Info.CultureDefault))
                {
                    result.AppendLine(string.Format("\t\t{0}: string;", resource.Item));
                }

                result.AppendLine("\t}");
                result.AppendLine("");
            }



            return result.ToString();
        }

        private string BuildClass()
        {
            StringBuilder result = new StringBuilder();

            foreach (var culture in Info.Cultures)
            {
                foreach (var detail in Info.Details)
                {
                    result.AppendLine(string.Format("\tclass {0}{1} implements I{0} {{", detail.Resource.Name, culture.Replace("-", string.Empty).ToUpper()));

                    foreach (var resource in GetResouces(detail, culture))
                    {
                        result.AppendLine(string.Format("\t\t{0}: string = \"{1}\";", resource.Item, resource.Valor));
                    }

                    result.AppendLine("\t}");
                    result.AppendLine("");
                }
            }

            return result.ToString();
        }

        private string BuidService()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("\texport class ResourceService {");
            result.AppendLine("");
            result.AppendLine("\t\tconstructor() {");
            result.AppendLine(string.Format("\t\t\tthis.SetLanguage(LanguageSuported.{0});", Info.CultureDefault.Replace("-", string.Empty)));
            result.AppendLine("\t\t}");
            result.AppendLine("");

            result.AppendLine("\t\tprivate Language: LanguageSuported;");
            result.AppendLine("");

            foreach (var info in Info.Details)
            {
                result.AppendLine(string.Format("\t\t{0}: I{0};", info.Resource.Name));
            }

            result.AppendLine("");
            result.AppendLine("\t\tGetLanguage(): LanguageSuported {");
            result.AppendLine("\t\t\treturn this.Language;");
            result.AppendLine("\t\t}");

            result.AppendLine("");
            result.AppendLine("\t\tSetLanguage(language: LanguageSuported): void {");
            result.AppendLine("\t\t\tthis.Language = language;");
            result.AppendLine("");
            result.AppendLine("\t\t\tswitch (this.Language) {");

            foreach (var culture in Info.Cultures)
            {
                result.AppendLine(string.Format("\t\t\t\tcase LanguageSuported.{0}:", culture.Replace("-", string.Empty)));

                foreach (var item in Info.Details)
                {
                    result.AppendLine(string.Format("\t\t\t\t\tthis.{0} = new {0}{1}();", item.Resource.Name, culture.Replace("-", string.Empty).ToUpper()));
                }

                result.AppendLine("\t\t\t\t\tbreak;");
            }

            result.AppendLine("\t\t\t}");

            result.AppendLine("\t\t}");
            result.AppendLine("\t}");

            return result.ToString();
        }

        private string BuildLanguageSuported()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("\texport enum LanguageSuported {");

            var cultures = Info.Cultures.Distinct().OrderBy(c => c).ToList();
            int count = 0;

            foreach (var culture in cultures)
            {
                count++;
                string item = culture.Replace("-", string.Empty);
                item += cultures.Count == count ? "" : ",";
                result.AppendLine(string.Format("\t\t{0}", item));
            }

            result.AppendLine("\t}");
            result.AppendLine("");

            return result.ToString();
        }
    }
}
