using RedGate.SQLCompare.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommon.DataBaseCompare
{
    public class ParamsBuilder
    {
        public ParamsBuilder(List<string> args)
        {
            Erros = new List<string>();
            Valid = true;
            ParseServer(args);
            ParseDatabaseName(args);
            ParseUserName(args);
            ParsePassword(args);
            ParseScriptSource(args);

            if (Valid)
                DataBaseProperty = new ConnectionProperties(serverName, databaseName, userName, password);
        }

        public ConnectionProperties DataBaseProperty { get; set; }

        public bool Valid { get; set; }

        public List<string> Erros { get; set; }

        private string serverName { get; set; }

        private string databaseName { get; set; }

        private string userName { get; set; }

        private string password { get; set; }

        public string ScriptSource { get; set; }

        private void ParseServer(List<string> args)
        {
            bool valid = false;

            foreach (var item in args)
            {
                if (item.IndexOf("serverName=", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    serverName = item.Split('=').Last();
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Erros.Add("Database server not defined");
                Valid = false;
            }
        }

        private void ParseDatabaseName(List<string> args)
        {
            bool valid = false;

            foreach (var item in args)
            {
                if (item.IndexOf("databaseName=", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    databaseName = item.Split('=').Last();
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Erros.Add("Database name not defined");
                Valid = false;
            }
        }

        private void ParseUserName(List<string> args)
        {
            bool valid = false;

            foreach (var item in args)
            {
                if (item.IndexOf("userName=", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    userName = item.Split('=').Last();
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Erros.Add("Database user name not defined");
                Valid = false;
            }
        }

        private void ParsePassword(List<string> args)
        {
            bool valid = false;

            foreach (var item in args)
            {
                if (item.IndexOf("password=", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    password = item.Split('=').Last();
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Erros.Add("Database password not defined");
                Valid = false;
            }
        }

        private void ParseScriptSource(List<string> args)
        {
            bool valid = false;

            foreach (var item in args)
            {
                if (item.IndexOf("scriptSource=", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    ScriptSource = item.Split('=').Last();
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Erros.Add("Params script source not defined");
                Valid = false;
            }
        }
    }
}
