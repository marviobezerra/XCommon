using System.Collections.Generic;
using System.Threading.Tasks;

namespace XCommon.Application.Logger
{
    public interface ILoggerWritter
    {
        Task<List<LoggerEntity>> LoadDataAsync();

        Task SaveDataAsync(LoggerEntity data);
    }
}
