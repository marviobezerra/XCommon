using System.Collections.Generic;

namespace XCommon.CodeGenerator.TypeScript.Writter
{
    internal class TypeScriptClass
    {
        public TypeScriptClass()
        {
            Imports = new List<TypeScriptImport>();
            Properties = new List<TypeScriptProperty>();
        }

        public string FileName { get; set; }

        public string Class { get; set; }

        public List<TypeScriptImport> Imports { get; set; }

        internal List<TypeScriptProperty> Properties { get; set; }
    }
}
