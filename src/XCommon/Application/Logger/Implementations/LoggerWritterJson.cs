using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace XCommon.Application.Logger.Implementations
{
    public class LoggerWritterJson : ILoggerWritter
    {
        public LoggerWritterJson(string filePath)
        {
            FilePath = filePath;
        }

        private string FilePath { get; set; }

        private static object LockObject = new object();

        public List<LoggerEntity> LoadData()
        {
            lock (LockObject)
            {
                return File.Exists(FilePath)
                    ? JsonConvert.DeserializeObject<List<LoggerEntity>>(File.ReadAllText(FilePath))
                    : new List<LoggerEntity>();
            }
        }

        public void SaveData(List<LoggerEntity> data)
        {
            lock (LockObject)
            {
                string content = JsonConvert.SerializeObject(data, Formatting.None);
                File.WriteAllText(FilePath, content);
            }
        }
    }
}
