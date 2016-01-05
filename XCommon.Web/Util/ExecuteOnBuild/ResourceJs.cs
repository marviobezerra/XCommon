using XCommon.Application.Culture;
using XCommon.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;

namespace XCommon.Web.Util.ExecuteOnBuild
{
    public abstract class ResourceJs : IExecuteOnBuild
    {
        public ResourceJs(string nameSpace, string fileName, Type resource, ResourceManager manager)
        {
            NameSpace = nameSpace;
            FileName = fileName;
            Resource = resource;
            Manager = manager;
        }

        private string NameSpace { get; set; }
        private Type Resource { get; set; }
        private ResourceManager Manager { get; set; }
        private string FileName { get; set; }

        public abstract void RegisterCultures();

        public void Execute(string assemblyPath)
        {
            RegisterCultures();

            StringBuilder retornoJavaScript = new StringBuilder();
            StringBuilder retornoCSharp = new StringBuilder();

            retornoCSharp.AppendLine(string.Format("namespace {0}", NameSpace));
            retornoCSharp.AppendLine("{");
            retornoCSharp.AppendLine("    public class ResourceJS");
            retornoCSharp.AppendLine("    {");

            foreach (var item in CultureHelper.ImplementedCultures)
                retornoJavaScript.AppendLine(string.Format(Properties.Resources.JavaScriptResourceRegister, item.Id, item.Name, item.ShortName, item.DateFormat, item.Default.ToString().ToLower()));

            retornoJavaScript.AppendLine(string.Empty);
            retornoJavaScript.AppendLine("XApplication.Resource = {");

            foreach (var resource in GetResouces())
            {
                StringBuilder itemJavaScript = new StringBuilder();
                retornoCSharp.AppendLine(string.Format(@"        public const string {0} = ""XApplication.Resource.{0}"";", resource.Item));

                foreach (KeyValuePair<string, string> itemCulture in resource.Valor)
                    itemJavaScript.AppendLine(string.Format(XCommon.Web.Properties.Resources.JavaScriptResourceItem, itemCulture.Key, itemCulture.Value));

                retornoJavaScript.AppendLine(string.Format(XCommon.Web.Properties.Resources.JavaScriptResourceValue, resource.Item, itemJavaScript));
            }

            retornoJavaScript.AppendLine("}");
            retornoCSharp.AppendLine("    }");
            retornoCSharp.AppendLine("}");

            var dir = Directory.GetParent(Path.GetDirectoryName(assemblyPath)).FullName;
            File.WriteAllText(Path.Combine(dir, "Scripts", "Application", FileName), string.Format("{0}{1}", Properties.Resources.NotificacaoArquivo, retornoJavaScript), Encoding.UTF8);
            File.WriteAllText(Path.Combine(dir, "1.Code", "Helper", "ResourceJS.cs"), string.Format("{0}{1}", Properties.Resources.NotificacaoArquivo, retornoCSharp), Encoding.UTF8);
        }

        private List<JSHelper> GetResouces()
        {
            List<JSHelper> retorno = new List<JSHelper>();

            foreach (var culture in CultureHelper.ImplementedCultures)
            {
                foreach (var item in Resource.GetProperties().Where(c => c.PropertyType == typeof(string)))
                {
                    JSHelper js = retorno.Find(c => c.Item == item.Name);

                    if (js == null)
                    {
                        js = new JSHelper { Item = item.Name };
                        retorno.Add(js);
                    }

                    js.Valor.Add(culture.Id, Manager.GetString(item.Name, new CultureInfo(culture.Id)));
                }
            }

            return retorno;
        }

        public class JSHelper
        {
            public JSHelper()
            {
                Valor = new Dictionary<string, string>();
            }

            public string Item { get; set; }

            public Dictionary<string, string> Valor { get; set; }
        }
    }
}
