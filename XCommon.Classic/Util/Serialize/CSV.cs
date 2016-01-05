using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XCommon.Util.Serialize
{
    public static class CSV
    {
        private static IEnumerable<List<string>> ParseContent(IList<string> contents)
        {
            return from line in contents
                   select line.Split(',').ToList();
        }

        private static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

        private static object GetValue(Type type, string value)
        {
            var typeName = type.Name;

            switch (typeName)
            {
                case "Guid":
                    return new Guid(value);
                case "List`1":
                    if (type.GenericTypeArguments.First().Name == "Guid")
                    {
                        List<Guid> resultGuidList = new List<Guid>();
                        foreach (string item in value.Split('|'))
                            resultGuidList.Add(new Guid(item));

                        return resultGuidList;
                    }

                    throw new Exception(typeName + ": Is not suported");
                default:
                    return Convert.ChangeType(value, type);
            }
        }

        public static List<TEntity> DeSerialize<TEntity>(string fileName)
            where TEntity : class, new()
        {
            var encode = GetEncoding(fileName);
            return DeSerialize<TEntity>(fileName, encode);
        }

        public static List<TEntity> DeSerialize<TEntity>(string fileName, Encoding encode)
            where TEntity : class, new()
        {
            return DeSerialize<TEntity>(ParseContent(File.ReadAllLines(fileName, encode)));
        }

        public static List<TEntity> DeSerialize<TEntity>(IEnumerable<List<string>> content)
            where TEntity : class, new()
        {
            List<TEntity> retorno = new List<TEntity>();
            List<string> reader = null;

            var properties = typeof(TEntity).GetProperties();

            foreach (var line in content)
            {
                if (reader == null)
                {
                    reader = line;
                    continue;
                }

                var entity = new TEntity();

                foreach (var property in properties)
                {
                    var index = reader.IndexOf(property.Name);

                    if (index < 0 || string.IsNullOrEmpty(line[index]))
                        continue;

                    string value = line[index].Trim();
                    Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    object safeValue = GetValue(type, value);

                    property.SetValue(entity, safeValue);
                }

                retorno.Add(entity);
            }

            return retorno;
        }
    }
}
