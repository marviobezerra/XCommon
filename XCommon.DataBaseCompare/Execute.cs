using RedGate.Shared.ComparisonInterfaces;
using RedGate.Shared.SQL.ExecutionBlock;
using RedGate.SQLCompare.Engine;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.DataBaseCompare
{
    public class Execute
    {
        public Execute(ParamsBuilder param)
        {
            Params = param;
            Exclude = new List<ObjectType> { ObjectType.User, ObjectType.Role, ObjectType.Certificate, ObjectType.SecurityPolicy, ObjectType.SymmetricKey };
        }

        public List<ObjectType> Exclude { get; set; }

        public ParamsBuilder Params { get; private set; }

        public void Run()
        {
            if (!Params.Valid)
            {
                Console.WriteLine("Params invalid");
                PrintErros();
                return;
            }

            int differencesCount = 0;

            using (Database dbScript = new Database(), dbProduction = new Database())
            {
                dbScript.Register(Params.ScriptSource, null, Options.Default);
                dbProduction.Register(Params.DataBaseProperty, Options.Default);

                Differences stagingVsProduction = dbScript.CompareWith(dbProduction, Options.Default);

                foreach (Difference difference in stagingVsProduction)
                {
                    if (Exclude.Contains(difference.DatabaseObjectType))
                        continue;

                    if (difference.Type != DifferenceType.Equal)
                    {
                        differencesCount++;
                        Console.WriteLine("{0} {1} {2}", GetDiffSource(difference.Type), difference.DatabaseObjectType, difference.Name);
                    }

                    difference.Selected = true;
                }

                Work work = new Work();

                work.BuildFromDifferences(stagingVsProduction, Options.Default, true);

                using (var block = work.ExecutionBlock)
                {
                     BlockExecutor executor = new BlockExecutor();
                     executor.ExecuteBlock(block, dbProduction.ConnectionProperties.ToDBConnectionInformation());
                }
            }
            
            Console.WriteLine($"Database {Params.DataBaseProperty.DatabaseName} updated with {differencesCount} differences");
        }

        private string GetDiffSource(DifferenceType type)
        {
            if (type == DifferenceType.OnlyIn1)
                return "Only script";

            if (type == DifferenceType.OnlyIn2)
                return "Only DataBase";

            return type.ToString();
        }

        private void PrintErros()
        {
            Params.Erros.ForEach(c => Console.WriteLine($"\t - {c}"));
        }
    }
}
