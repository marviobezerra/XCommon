using System;
using System.Collections.Generic;

namespace XCommon.ProjectGeratorV2.Application.CSharp.Extension
{
    public static class CommandExtencionsCommon
    {
        public static string MergeProjectJson(string template, List<string> references)
        {
            var count = 1;
            string include = string.Empty;

            references.ForEach(item =>
            {
                string newLine = references.Count != count++ ? Environment.NewLine : string.Empty;
                include += $"    \"{item}\": \"1.0.0-*\",{newLine}";
            });

            return template.Replace("[{include}]", include);
        }
    }
}
