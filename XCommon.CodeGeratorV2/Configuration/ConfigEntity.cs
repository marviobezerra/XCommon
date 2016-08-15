using System;
using System.Collections.Generic;
using System.Reflection;

namespace XCommon.CodeGeratorV2.Configuration
{
    public class ConfigEntity
    {
        public ConfigEntity()
        {
            Assemblys = new List<Assembly>();
            TypesExtra = new List<Type>();
        }

        public string Path { get; set; }

        public List<Assembly> Assemblys { get; set; }

        public List<Type> TypesExtra { get; set; }
    }
}
