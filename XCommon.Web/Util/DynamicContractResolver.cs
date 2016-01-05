using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace XCommon.Web.Util
{
    public class DynamicContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.Ignored = false;
            return property;
        }
    }
}
