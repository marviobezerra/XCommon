using System.Collections.Generic;
using System.Linq;
using XCommon.Util;

namespace XCommon.Application.FileStorage.Implementations
{
    public class FileStorageInMemory : IFileStorage
    {
        private List<Pair<string, Dictionary<string, byte[]>>> Storage { get; set; }

        public FileStorageInMemory()
        {
            Root = "FileStorageInMemory";
            Storage = new List<Pair<string, Dictionary<string, byte[]>>>();
        }

        private string Root { get; set; }

        private Dictionary<string, byte[]> GetFolder(string name, bool createIfEmpty = true)
        {
            var result = Storage.FirstOrDefault(c => c.Item1 == name);

            if (result == null && createIfEmpty)
            {
                result = new Pair<string, Dictionary<string, byte[]>>(name, new Dictionary<string, byte[]>());
                Storage.Add(result);
            }

            return result.Item2;
        }

        public bool Delete(string fileName)
            => Delete(Root, fileName);

        public bool Delete(string container, string fileName)
        {
            var folder = GetFolder(container, false);

            if (folder == null)
			{
				return false;
			}

			if (folder.ContainsKey(fileName))
            {
                folder.Remove(fileName);
                return true;
            }

            return false;
        }

        public bool Exists(string fileName)
            => Exists(Root, fileName);

        public bool Exists(string container, string fileName)
        {
            var folder = GetFolder(container, false);

            if (folder == null)
			{
				return false;
			}

			return folder.ContainsKey(fileName);
        }

        public byte[] Load(string fileName)
            => Load(Root, fileName);

        public byte[] Load(string container, string fileName)
        {
            var folder = GetFolder(container, false);

            if (folder == null)
			{
				return null;
			}

			folder.TryGetValue(fileName, out byte[] result);
			return result;
        }

        public bool Save(string fileName, byte[] content, bool overRide = true)
            => Save(Root, fileName, content, overRide);

        public bool Save(string container, string fileName, byte[] content, bool overRide = true)
        {
            var folder = GetFolder(container, true);

            if (overRide || !folder.ContainsKey(fileName))
            {
                folder[fileName] = content;
                return true;
            }

            return false;
        }
    }
}
