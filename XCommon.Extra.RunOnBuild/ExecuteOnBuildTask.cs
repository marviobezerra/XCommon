using System;

namespace XCommon.Extra.RunOnBuild
{
    public class ExecuteOnBuildTask
    {
        private static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                throw new Exception("Aguardado o assemblie de referencia");
            }
            new Execute().Run(args[0]);
        }
    }

}
