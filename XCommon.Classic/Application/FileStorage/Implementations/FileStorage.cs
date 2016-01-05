using System;
using System.IO;

namespace XCommon.Application.FileStorage.Implementations
{
    public class FileStorage : IFileStorage
    {
        public FileStorage(string basePath)
        {
            BasePath = basePath;
            _Lock = new object();
            if (!Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
        }

        private object _Lock { get; set; }

        public string BasePath { get; private set; }

        private string GetFullPath(string fileName)
        {
            var fullPath = Path.Combine(BasePath, fileName);
            var diretory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(diretory))
                Directory.CreateDirectory(diretory);

            return fullPath;
        }

        public bool Save(string fileName, byte[] content)
        {
            lock (_Lock)
            {
                try
                {
                    File.WriteAllBytes(GetFullPath(fileName), content);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool Save(string container, string fileName, byte[] content)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string fileName)
        {
            lock (_Lock)
            {
                try
                {
                    return File.Exists(GetFullPath(fileName));
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteIfExists(string fileName)
        {
            lock (_Lock)
            {
                var fullPath = GetFullPath(fileName);

                if (File.Exists(fullPath))
                    File.Delete(fullPath);

                return true;
            }
        }

        public bool Delete(string fileName)
        {
            lock (_Lock)
            {
                try
                {
                    Delete(GetFullPath(fileName));
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public byte[] Load(string fileName)
        {
            lock (_Lock)
            {
                return File.ReadAllBytes(GetFullPath(fileName));
            }
        }

        public byte[] LoadIfExists(string fileName)
        {
            lock (_Lock)
            {
                var fullPath = GetFullPath(fileName);
                if (File.Exists(fullPath))
                    return File.ReadAllBytes(fullPath);

                return null;
            }
        }

        public bool Exists(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteIfExists(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public byte[] Load(string container, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
