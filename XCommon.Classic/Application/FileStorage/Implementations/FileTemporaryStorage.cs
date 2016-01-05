using System.IO;

namespace XCommon.Application.FileStorage.Implementations
{
    public class FileTemporaryStorage : FileStorage, IFileTemporaryStorage
    {
        public FileTemporaryStorage(string basePath)
            : base(basePath)
        {

        }

        public bool ClearAll()
        {
            if (Directory.Exists(BasePath))
                Directory.Delete(BasePath, true);

            return true;
        }
    }
}
