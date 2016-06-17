using System.Collections.Generic;
using XCommon.Extensions.Util;

namespace XCommon.Patterns.Repository.Executes
{
	public class ExecuteUser
    {
        public ExecuteUser()
        {
            Propertys = new Dictionary<string, object>();
            Values = new Indexer<string, object>(GetValue, SetValue);
        }

        public object Key { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public Indexer<string, object> Values { get; private set; }

        private Dictionary<string, object> Propertys { get; set; }

        public T GetValue<T>(string index)
        {
            var retorno = GetValue(index);

            if (retorno == null)
                return default(T);

            return (T)retorno;
        }

        private object GetValue(string index)
        {
            var retorno = (object)null;
            Propertys.TryGetValue(index, out retorno);
            return retorno;
        }

        private void SetValue(string index, object value)
        {
            Propertys[index] = value;
        }
    }
}
