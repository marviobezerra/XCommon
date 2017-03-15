using System.IO;

namespace XCommon.Application.FileStorage.Implementations
{
    public class FileStorageLocal : IFileStorage
    {
        public FileStorageLocal(string rootFolder)
        {
            Root = rootFolder;
        }

        private string Root { get; set; }

        private string GetFullPath(string folder, string file)
        {
            return Path.Combine(Root, folder, file);
        }

        private bool CheckFolder(string folder, bool createIfEmpty = true)
        {
            if (Directory.Exists(folder))
			{
				return true;
			}

			if (createIfEmpty)
            {
                Directory.CreateDirectory(folder);
                return true;
            }

            return false;
        }

        public bool Delete(string fileName)
            => Delete(string.Empty, fileName);

        public bool Delete(string container, string fileName)
        {
            var file = GetFullPath(container, fileName);

            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool Exists(string fileName)
            => Exists(string.Empty, fileName);

        public bool Exists(string container, string fileName)
        {
            var file = GetFullPath(container, fileName);
            return File.Exists(file);
        }

        public byte[] Load(string fileName)
            => Load(string.Empty, fileName);

        public byte[] Load(string container, string fileName)
        {
            var file = GetFullPath(container, fileName);

            if (File.Exists(file))
			{
				return File.ReadAllBytes(file);
			}

			return null;
        }

        public bool Save(string fileName, byte[] content, bool overRide = true)
            => Save(string.Empty, fileName, content, overRide);

        public bool Save(string container, string fileName, byte[] content, bool overRide = true)
        {
            var file = GetFullPath(container, fileName);
            CheckFolder(container, true);

            if (overRide || !File.Exists(file))
            {
                File.WriteAllBytes(file, content);
                return true;
            }

            return false;
        }
    }
}
