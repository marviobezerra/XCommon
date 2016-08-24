using RedGate.SQLCompare.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.DataBaseCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            ParamsBuilder param = new ParamsBuilder(args.ToList());
            Execute execute = new Execute(param);
            execute.Run();
        }

    }
}
