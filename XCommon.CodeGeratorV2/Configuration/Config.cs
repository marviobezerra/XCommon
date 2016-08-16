namespace XCommon.CodeGeratorV2.Configuration
{
    public class Config
    {
        public Config()
        {
            Angular = new ConfigAngular();
            DataBase = new ConfigDataBase();
            CSharp = new ConfigCSharp();
            TypeScript = new ConfigTypeScript();
        }

        public ConfigAngular Angular { get; set; }

        public ConfigCSharp CSharp { get; set; }

        public ConfigDataBase DataBase { get; set; }

        public ConfigTypeScript TypeScript { get; set; }
    }
}
