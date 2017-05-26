using System.IO;

namespace XCommon.Application.FileStorage.Implementations
{
	public class FileTemporaryStorage : FileStorageLocal, IFileTemporaryStorage
	{
		public FileTemporaryStorage(string rootFolder) : base(rootFolder)
		{
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
