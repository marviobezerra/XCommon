namespace XCommon.CodeGeratorV2.Configuration
{
    public class ConfigTypeScript
    {
        public ConfigTypeScript()
        {
            Entity = new ConfigEntity();
            Resource = new ConfigResource();
        }

        public ConfigEntity Entity { get; set; }
        public ConfigResource Resource { get; set; }
    }
}
