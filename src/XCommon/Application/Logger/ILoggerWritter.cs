using System.Collections.Generic;

namespace XCommon.Application.Logger
{
    public interface ILoggerWritter
    {
        List<LoggerEntity> LoadData();

        void SaveData(List<LoggerEntity> data);
    }
}
