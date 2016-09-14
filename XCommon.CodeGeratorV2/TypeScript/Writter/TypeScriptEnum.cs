using System;
using System.Collections.Generic;

namespace XCommon.CodeGeratorV2.TypeScript.Writter
{
    internal class TypeScriptEnum
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public Dictionary<string, int> Values { get; set; }
    }
}
