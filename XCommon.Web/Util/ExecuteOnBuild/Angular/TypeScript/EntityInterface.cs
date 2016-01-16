using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using XCommon.Util;

namespace XCommon.Web.Util.ExecuteOnBuild.Angular.TypeScript
{
    internal class EntityProperty
    {
        public bool Nullable { get; set; }

        public string NameSpace { get; set; }

        public string Class { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
    
    public abstract class EntityInterface : IExecuteOnBuild
    {
        public EntityInterface(string module, params Assembly[] assemblys)
        {
            Module = module;

            Properties = new List<EntityProperty>();
            Assemblys = assemblys.ToList();
        }

        private string Module { get; set; }

        private List<Assembly> Assemblys { get; set; }

        private List<EntityProperty> Properties { get; set; }

        public void Execute(string assemblyPath)
        {
            var actions = Assemblys.SelectMany(c => c.GetTypes())
                .Where(type => !type.IsAbstract)
                .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy))
                .Select(x => new EntityProperty
                {
                    Class = x.DeclaringType.Name,
                    NameSpace = GetNameSpace(x.DeclaringType.Namespace),
                    Name = x.Name,
                    Nullable = Nullable.GetUnderlyingType(x.PropertyType) != null,
                    Type = GetPropertyType(x.PropertyType)
                })
                .OrderBy(x => x.NameSpace)
                .ThenBy(x => x.Class)
                .ToList();

            var dir = Directory.GetParent(Path.GetDirectoryName(assemblyPath)).FullName;
            var file = GetFileName(dir);
            File.WriteAllText(file, Build(actions), Encoding.UTF8);
        }

        protected virtual string GetFileName(string basePath)
        {
            var dir = Path.Combine(basePath, "Scripts", "Common");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return Path.Combine(dir, "AppEntityInterface.ts");
        }

        protected virtual string GetNameSpace(string nameSpace)
        {
            return nameSpace.Replace("Prospect.Pet.Business.Entity.", string.Empty);
        }

        private string GetPropertyType(Type type)
        {
            var underlyingType = Nullable.GetUnderlyingType(type);

            var currentType = underlyingType == null
                ? type
                : underlyingType;

            if (currentType.IsGenericType && currentType.Name.Contains("List"))
            {
                try
                {
                    Type genericType = currentType.GenericTypeArguments.FirstOrDefault();
                    return string.Format("Array<{0}>", GetPropertyType(genericType));
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }

            if (currentType.Name.Contains("Entity"))
            {
                return currentType.Name;
            }

            switch (currentType.Name)
            {
                case "Int32":
                case "Int64":
                case "Decimal":
                case "Single":
                    return "number";
                case "Guid":
                case "String":
                    return "string";
                case "DateTime":
                    return "Date";
                default:
                    return "Não identificado: " + currentType.Name;
            }
        }

        private string Build(List<EntityProperty> actions)
        {
            StringBuilder result = new StringBuilder();

            foreach (var nameSpace in actions.Select(c => c.NameSpace).Distinct().OrderBy(c => c))
            {
                result.AppendLine(string.Format("module {0}.{1} {{", Module, nameSpace));
                result.AppendLine("");

                foreach (var className in actions.Where(c => c.NameSpace == nameSpace).Select(c => c.Class).Distinct().OrderBy(c => c))
                {
                    result.AppendLine(string.Format("\texport interface {0} {{", className));

                    foreach (var property in actions.Where(c => c.NameSpace == nameSpace && c.Class == className))
                    {
                        result.AppendLine(string.Format("\t\t{0}: {1}; ", property.Name, property.Type));
                    }

                    result.AppendLine("\t}");
                    result.AppendLine("");
                }

                result.AppendLine("}");
                result.AppendLine("");
            }



            return result.ToString();
        }
    }
}
