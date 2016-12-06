using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCommon.Application.Logger.Implementations
{
    public class LoggerWritterInMemory : ILoggerWritter
    {
        public LoggerWritterInMemory()
        {
            LoggerData = new List<LoggerEntity>();
        }

        private List<LoggerEntity> LoggerData { get; set; }
        
        public async Task<List<LoggerEntity>> LoadDataAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                return LoggerData;
            });
        }

        public async Task SaveDataAsync(LoggerEntity data)
        {
            await Task.Factory.StartNew(() => 
            {
                LoggerData.Add(data);
            });
        }
    }
}
