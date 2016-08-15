namespace XCommon.CodeGeratorV2.Configuration
{
    public class Config
    {
        public Config()
        {
            Angular = new ConfigAngular();
            DataBase = new ConfigDataBase();
            Entity = new ConfigEntity();
            Resource = new ConfigResource();
        }

        public ConfigAngular Angular { get; set; }

        public ConfigDataBase DataBase { get; set; }

        public ConfigEntity Entity { get; set; }

        public ConfigResource Resource { get; set; }
    }
}
