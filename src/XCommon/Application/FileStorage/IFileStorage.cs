using System.Threading.Tasks;

namespace XCommon.Application.FileStorage
{
    public interface IFileStorage
    {
		Task<bool> SaveAsync(string container, string fileName, byte[] content, bool overRide = true);

		Task<bool> SaveAsync(string fileName, byte[] content, bool overRide = true);

		Task<bool> ExistsAsync(string fileName);

		Task<bool> ExistsAsync(string container, string fileName);

		Task<bool> DeleteAsync(string fileName);

        Task<bool> DeleteAsync(string container, string fileName);

		Task<bool> DeleteContainerAsync(string container);

		Task<byte[]> LoadAsync(string fileName);

        Task<byte[]> LoadAsync(string container, string fileName);
    }
}
