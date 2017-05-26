using System;
using System.IO;
using System.Threading.Tasks;

namespace XCommon.Application.FileStorage.Implementations
{
    public class FileStorageLocal : IFileStorage
    {
        public FileStorageLocal(string rootFolder)
        {
            Root = rootFolder;

			if (!Directory.Exists(Root))
			{
				Directory.CreateDirectory(Root);
			}
		}

        protected string Root { get; set; }

		private string GetFullContainer(string container)
		{
			return Path.Combine(Root, container);
		}

        private string GetFullPath(string container, string file)
        {
			var folder = GetFullContainer(container);
            return Path.Combine(folder, file);
        }

        private bool CheckFolder(string container, bool createIfEmpty = true)
        {
			var folder = GetFullContainer(container);

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

        public async Task<bool> DeleteAsync(string fileName)
            => await DeleteAsync(string.Empty, fileName);

        public async Task<bool> DeleteAsync(string container, string fileName)
        {
			return await Task.Factory.StartNew(() => {

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
			});
        }

        public async Task<bool> ExistsAsync(string fileName)
            => await ExistsAsync(string.Empty, fileName);

        public async Task<bool> ExistsAsync(string container, string fileName)
        {
			return await Task.Factory.StartNew(() => {
				var file = GetFullPath(container, fileName);
				return File.Exists(file);
			});
        }

        public async Task<byte[]> LoadAsync(string fileName)
            => await LoadAsync(string.Empty, fileName);

        public async Task<byte[]> LoadAsync(string container, string fileName)
        {
			return await Task.Factory.StartNew(() => {
				var file = GetFullPath(container, fileName);

				if (File.Exists(file))
				{
					return File.ReadAllBytes(file);
				}

				return null;
			});
        }

        public async Task<bool> SaveAsync(string fileName, byte[] content, bool overRide = true)
            => await SaveAsync(string.Empty, fileName, content, overRide);

        public async Task<bool> SaveAsync(string container, string fileName, byte[] content, bool overRide = true)
        {
			return await Task.Factory.StartNew(() => {
				var file = GetFullPath(container, fileName);
				CheckFolder(container, true);

				if (overRide || !File.Exists(file))
				{
					File.WriteAllBytes(file, content);
					return true;
				}

				return false;
			});
        }

		public async Task<bool> DeleteContainerAsync(string container)
		{
			return await Task.Factory.StartNew(() => {
				var folder = GetFullContainer(container);

				if (Directory.Exists(folder))
				{
					Directory.Delete(folder);
				}

				return true;
			});
		}
	}
}
