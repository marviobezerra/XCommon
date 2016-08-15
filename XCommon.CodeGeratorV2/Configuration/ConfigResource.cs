using System;
using System.Collections.Generic;
using System.Resources;

namespace XCommon.CodeGeratorV2.Configuration
{
    public class ConfigResource
    {
        public ConfigResource()
        {
            Cultures = new List<string>();
            Resources = new Dictionary<Type, ResourceManager>();
        }

        public string Path { get; set; }

        public string File { get; set; }

        public string CultureDefault { get; set; }

        public List<string> Cultures { get; set; }

        public Dictionary<Type, ResourceManager> Resources { get; set; }
    }
}
