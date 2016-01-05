using XCommon.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XCommon.Web.Util.ExecuteOnBuild
{
    public abstract class UrlJS : IExecuteOnBuild
    {
        public Assembly CurrentAssembly { get; set; }

        public UrlJS(Assembly assembly)
        {
            CurrentAssembly = assembly;
        }

        public void Execute(string assemblyPath)
        {
            var actions = CurrentAssembly.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && !type.IsAbstract)
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new UrlInfoEntity
                {
                    Controller = x.DeclaringType.Name,
                    NameSpace = x.DeclaringType.Namespace,
                    Action = x.Name,
                    Post = x.GetCustomAttribute(typeof(HttpPostAttribute)) != null,
                    ReturnType = x.ReturnType.Name
                })
                .OrderBy(x => x.Controller)
                .ThenBy(x => x.Action)
                .ToList();

            var dir = Directory.GetParent(Path.GetDirectoryName(assemblyPath)).FullName;
            File.WriteAllText(Path.Combine(dir, "Scripts", "Application", "XUrl.Js"), Build(actions), Encoding.UTF8);
        }

        private string Build(List<UrlInfoEntity> infos)
        {
            infos.ForEach(ProcessInfo);

            StringBuilder result = new StringBuilder();

            result.AppendLine("XApplication.Url = {");

            foreach (var nameSpace in infos.Select(c => c.NameSpace).Distinct())
            {
                result.AppendLine(string.Format("\t{0} : {{", nameSpace != string.Empty ? nameSpace : "Root"));
                

                foreach (var controller in infos.Where(c => c.NameSpace == nameSpace).Select(c => c.Controller).Distinct())
                {
                    result.AppendLine(string.Format("\t\t{0} : {{", controller.Replace("Controller", string.Empty)));

                    result.AppendLine("\t\t\tPost : {");

                    foreach (var action in infos.Where(c => c.NameSpace == nameSpace && c.Controller == controller && c.Post))
                    {
                        result.AppendLine(string.Format("\t\t\t\t{0} : function() {{", action.Action));
                        result.AppendLine(string.Format("\t\t\t\t\treturn XApplication.Url.Base() + '{0}';", action.Url));
                        result.AppendLine("\t\t\t\t\t},");
                    }

                    result.AppendLine("\t\t\t},");

                    result.AppendLine("\t\t\tGet : {");

                    foreach (var action in infos.Where(c => c.NameSpace == nameSpace && c.Controller == controller && !c.Post))
                    {
                        result.AppendLine(string.Format("\t\t\t\t{0} : function() {{", action.Action));
                        result.AppendLine(string.Format("\t\t\t\t\treturn XApplication.Url.Base() + '{0}';", action.Url));
                        result.AppendLine("\t\t\t\t\t},");
                    }

                    result.AppendLine("\t\t\t},");

                    result.AppendLine("\t\t},");
                }
                
                result.AppendLine("\t},");
            }
            
            result.AppendLine("}");
            return result.ToString();    
        }

        public abstract void ProcessInfo(UrlInfoEntity info);
    }

    public class UrlInfoEntity
    {
        public bool Post { get; set; }

        public string Controller { get; set; }

        public string NameSpace { get; set; }

        public string Action { get; set; }

        public string ReturnType { get; set; }

        public string Url { get; set; }
    }
}
