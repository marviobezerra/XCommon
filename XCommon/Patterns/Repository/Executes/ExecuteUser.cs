using XCommon.Extensions.Util;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XCommon.Patterns.Repository.Executes
{
    [DataContract]
    public class ExecuteUser
    {
        public ExecuteUser()
        {
            Propertys = new Dictionary<string, object>();
            Values = new Indexer<string, object>(GetValue, SetValue);
        }

        [DataMember]
        public object Key { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Login { get; set; }

        [IgnoreDataMember]
        public Indexer<string, object> Values { get; private set; }

        [DataMember]
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
