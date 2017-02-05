using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace XCommon.Application.Logger.Implementations
{
    public class LoggerWritterJson : ILoggerWritter
    {
        public LoggerWritterJson(string filePath)
        {
            FilePath = filePath;
            LoadData();
        }

        private string FilePath { get; set; }

        private List<LoggerEntity> LoggerData { get; set; }

        private void LoadData()
        {
            LoggerData = File.Exists(FilePath)
                  ? JsonConvert.DeserializeObject<List<LoggerEntity>>(File.ReadAllText(FilePath))
                  : new List<LoggerEntity>();
        }

        public async Task<List<LoggerEntity>> LoadDataAsync()
        {
            return await Task.Factory.StartNew(() => 
            {
                return LoggerData;
            });           
        }

        public async Task SaveDataAsync(LoggerEntity item)
        {
            await Task.Factory.StartNew(() =>
            {
                LoggerData.Add(item);
                string content = JsonConvert.SerializeObject(LoggerData, Formatting.None);
                File.WriteAllText (FilePath, content);
            });
        }
    }
}
