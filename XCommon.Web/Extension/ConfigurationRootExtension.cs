using Microsoft.Extensions.Configuration;

namespace XCommon.Web
{
    public static class ConfigurationRootExtension
    {
        public static T Get<T>(this IConfigurationRoot config, string key)
            where T: class, new()
        {
            var result = new T();
            config.GetSection(key).Bind(result);
            return result;
        }
    }
}
