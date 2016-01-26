using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Text;
using XCommon.Util;
using XCommon.Web.Util.ExecuteOnBuild;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace XCommon.Web.Util.ExecuteOnBuild.Angular.TypeScript
{
    internal class UrlInfoEntity
    {
        public bool Post { get; set; }

        public string Controller { get; set; }

        public string NameSpace { get; set; }

        public string Action { get; set; }

        public string ReturnType { get; set; }

        public string Url { get; set; }
    }

    public abstract class PathService : IExecuteOnBuild
    {
        private string Module { get; set; }

        private string RegisterScript { get; set; }

        private Assembly CurrentAssembly { get; set; }

        public PathService(string module, string registerScript, Assembly currentAssembly)
        {
            Module = module;
            RegisterScript = registerScript;
            CurrentAssembly = currentAssembly;
        }

        protected virtual string GetFileName(string basePath)
        {
            var dir = Path.Combine(basePath, "Scripts", "Common");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Path.Combine(dir, "PathService.ts");
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
            var file = GetFileName(dir);
            File.WriteAllText(file, Build(actions), Encoding.UTF8);
        }

        private string Build(List<UrlInfoEntity> infos)
        {
            infos.ForEach(BuildUrl);

            StringBuilder result = new StringBuilder();

            result.AppendLine(string.Format("module {0} {{", Module));
            result.AppendLine("");

            result.Append(BuildInterfaces(infos));
            result.Append(BuildAreas(infos));
            result.Append(BuildService(infos));

            result.AppendLine("}");
            result.AppendLine("");
            result.AppendLine(RegisterScript);

            return result.ToString();
        }

        private void BuildUrl(UrlInfoEntity into)
        {
            var area = into.NameSpace.Split('.');

            string result = area.Length == 5
                ? ""
                : area[area.Length - 2];

            // Fix Controller
            into.Controller = into.Controller.Replace("Controller", string.Empty);

            // Fix NameSpace
            into.NameSpace = string.IsNullOrEmpty(result) ? "Root" : result;

            result += "/" + into.Controller;
            result += "/" + into.Action;
            result = result.StartsWith("/") ? result : "/" + result;

            // Set URL
            into.Url = result.ToLower();
        }

        private string BuildInterfaces(List<UrlInfoEntity> infos)
        {
            StringBuilder result = new StringBuilder();

            foreach (var controller in infos.Select(c => c.Controller).Distinct().OrderBy(c => c))
            {
                result.AppendLine(string.Format("\tinterface I{0} {{", controller));

                foreach (var action in infos.Where(c => c.Controller == controller))
                {
                    result.AppendLine(string.Format("\t\t{0}: string;", action.Action));
                }

                result.AppendLine("\t}");
            }

            return result.ToString();
        }

        private string BuildAreas(List<UrlInfoEntity> infos)
        {
            StringBuilder result = new StringBuilder();

            foreach (var area in infos.Select(c => c.NameSpace).Distinct().OrderBy(c => c))
            {
                result.AppendLine(string.Format("\tinterface I{0} {{", area));

                foreach (var controller in infos.Where(c => c.NameSpace == area).Select(c => c.Controller).Distinct().OrderBy(c => c))
                {
                    result.AppendLine(string.Format("\t\t{0}: I{0}", controller));
                }

                result.AppendLine("\t}");
            }

            return result.ToString();
        }

        private string BuildService(List<UrlInfoEntity> infos)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("\texport class PathService {");
            result.AppendLine("\t\tconstructor() {");
            result.AppendLine("\t\t}");

            foreach (var area in infos.Select(c => c.NameSpace).Distinct().OrderBy(c => c))
            {
                result.AppendLine(string.Format("\t\t\t{0}: I{0} = {{", area));

                int countControllers = 0;
                var controllers = infos.Where(c => c.NameSpace == area).Select(c => c.Controller).Distinct().OrderBy(c => c).ToList();

                foreach (var controller in controllers)
                {
                    countControllers++;
                    string endController = countControllers == controllers.Count ? "" : ",";
                    result.AppendLine(string.Format("\t\t\t\t{0}: {{", controller));

                    int countAction = 0;
                    var actions = infos.Where(c => c.Controller == controller).ToList();

                    foreach (var action in actions)
                    {
                        countAction++;
                        string endAction = countAction == actions.Count ? "" : ",";
                        result.AppendLine(string.Format("\t\t\t\t\t{0}: \"{1}\"{2}", action.Action, action.Url, endAction));
                    }

                    result.AppendLine(string.Format("\t\t\t\t}}{0}", endController));
                }

                result.AppendLine("\t\t\t}");
            }

            result.AppendLine("\t}");

            return result.ToString();
        }
    }
}
