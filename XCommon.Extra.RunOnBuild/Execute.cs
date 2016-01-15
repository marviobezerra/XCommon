using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using XCommon.Util;

namespace XCommon.Extra.RunOnBuild
{
    public class Execute
    {
        public void Run(string assemblie)
        {
            using (CompositionContainer container = new CompositionContainer(new DirectoryCatalog(Path.GetDirectoryName(assemblie), Path.GetFileName(assemblie)), new ExportProvider[0]))
            {
                container.ComposeParts(new object[] { this });
                foreach (Lazy<IExecuteOnBuild> lazy in this.Operations)
                {
                    lazy.Value.Execute(assemblie);
                }
            }
        }

        [ImportMany(typeof(IExecuteOnBuild))]
        private IEnumerable<Lazy<IExecuteOnBuild>> Operations { get; set; }
    }

}
