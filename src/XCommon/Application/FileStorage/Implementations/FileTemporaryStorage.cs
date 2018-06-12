using System.IO;
using XCommon.Application.Settings;
using XCommon.Patterns.Ioc;

namespace XCommon.Application.FileStorage.Implementations
{
	public class FileTemporaryStorage : FileStorageLocal, IFileTemporaryStorage
	{
		private IApplicationSettings ApplicationSettings => Kernel.Resolve<IApplicationSettings>();

		public FileTemporaryStorage()
		{
			Init(ApplicationSettings.Storage.TemporaryStorage, false, true);
		}

		public FileTemporaryStorage(string rootFolder)
		{
			Init(rootFolder, true, true);
		}

		public bool ClearAll()
		{
			if (Directory.Exists(Root))
			{
				Directory.Delete(Root, true);
			}

			Directory.CreateDirectory(Root);
			return true;
		}
	}
}
