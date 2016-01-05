using Newtonsoft.Json;

namespace XCommon.Util.Serialize
{
    public static class JSON
    {
        public static string Serialize<TEntity>(TEntity objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        public static TEntity DeSerialize<TEntity>(string texto)
        {
            return JsonConvert.DeserializeObject<TEntity>(texto);
        }
    }
}
