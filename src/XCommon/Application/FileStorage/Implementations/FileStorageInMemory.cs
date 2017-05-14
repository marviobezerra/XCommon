using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> DeleteAsync(string fileName)
            => await DeleteAsync(Root, fileName);

		public async Task<bool> DeleteAsync(string container, string fileName)
		{
			return await Task.Factory.StartNew(() =>
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
			});
		}

		public async Task<bool> ExistsAsync(string fileName)
            => await ExistsAsync(Root, fileName);

        public async Task<bool> ExistsAsync(string container, string fileName)
        {
			return await Task.Factory.StartNew(() => {
				var folder = GetFolder(container, false);

				if (folder == null)
				{
					return false;
				}

				return folder.ContainsKey(fileName);
			});
        }

        public async Task<byte[]> LoadAsync(string fileName)
            => await LoadAsync(Root, fileName);

        public async Task<byte[]> LoadAsync(string container, string fileName)
        {
			return await Task.Factory.StartNew(() => {
				var folder = GetFolder(container, false);

				if (folder == null)
				{
					return null;
				}

				folder.TryGetValue(fileName, out byte[] result);
				return result;
			});
        }

        public async Task<bool> SaveAsync(string fileName, byte[] content, bool overRide = true)
            => await SaveAsync(Root, fileName, content, overRide);

        public async Task<bool> SaveAsync(string container, string fileName, byte[] content, bool overRide = true)
        {
			return await Task.Factory.StartNew(() => {
				var folder = GetFolder(container, true);

				if (overRide || !folder.ContainsKey(fileName))
				{
					folder[fileName] = content;
					return true;
				}

				return false;
			});
        }

		public async Task<bool> DeleteContainerAsync(string container)
		{
			return await Task.Factory.StartNew(() => {

				var result = Storage.RemoveAll(c => c.Item1 == container);
				return true;
			});
		}
	}
}
