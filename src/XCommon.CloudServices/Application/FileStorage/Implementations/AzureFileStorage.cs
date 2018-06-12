using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using XCommon.Application.FileStorage;
using XCommon.Application.Settings;
using XCommon.Extensions.Converters;
using XCommon.Extensions.String;
using XCommon.Patterns.Ioc;

namespace XCommon.CloudServices.Application.FileStorage.Implementations
{
	public class AzureFileStorage : IFileStorage
	{
		[Inject]
		private IApplicationSettings ApplicationSettings { get; set; }

		private CloudStorageAccount StorageAccount { get; set; }

		private CloudBlobClient BlobClient { get; set; }

		public AzureFileStorage()
		{
			Kernel.Resolve(this);

			if (ApplicationSettings.Production && ApplicationSettings.Storage.AzureStorageConnectionString.IsEmpty())
			{
				throw new Exception("Azure storage not found on the ApplicationSettings");
			}

			StorageAccount = ApplicationSettings.Production
				? CloudStorageAccount.Parse(ApplicationSettings.Storage.AzureStorageConnectionString)
				: CloudStorageAccount.DevelopmentStorageAccount;

			BlobClient = StorageAccount.CreateCloudBlobClient();
		}

		private string ParseContainerName(string container)
		{
			return container.ToLower();
		}

		public async Task<bool> SaveAsync(string container, string fileName, byte[] content, bool overRide = true)
		{
			try
			{
				var cloudBlobContainer = BlobClient.GetContainerReference(ParseContainerName(container));
				await cloudBlobContainer.CreateIfNotExistsAsync();

				var blob = cloudBlobContainer.GetBlockBlobReference(fileName);

				if (!overRide && await blob.ExistsAsync())
				{
					return false;
				}

				await blob.UploadFromByteArrayAsync(content, 0, content.Length);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public Task<bool> SaveAsync(string fileName, byte[] content, bool overRide = true)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ExistsAsync(string fileName)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> ExistsAsync(string container, string fileName)
		{
			try
			{
				var cloudBlobContainer = BlobClient.GetContainerReference(ParseContainerName(container));
				await cloudBlobContainer.CreateIfNotExistsAsync();

				var blob = cloudBlobContainer.GetBlockBlobReference(fileName);
				return await blob.ExistsAsync();
			}
			catch (Exception)
			{
				return false;
			}
		}

		public Task<bool> DeleteAsync(string fileName)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteAsync(string container, string fileName)
		{
			try
			{
				var cloudBlobContainer = BlobClient.GetContainerReference(ParseContainerName(container));
				await cloudBlobContainer.CreateIfNotExistsAsync();

				var blob = cloudBlobContainer.GetBlockBlobReference(fileName);
				return await blob.DeleteIfExistsAsync();
			}
			catch (Exception)
			{
				return false;
			}
		}

		public Task<byte[]> LoadAsync(string fileName)
		{
			throw new NotImplementedException();
		}

		public async Task<byte[]> LoadAsync(string container, string fileName)
		{
			try
			{
				var cloudBlobContainer = BlobClient.GetContainerReference(ParseContainerName(container));
				await cloudBlobContainer.CreateIfNotExistsAsync();

				var blob = cloudBlobContainer.GetBlockBlobReference(fileName);
				var stream = await blob.OpenReadAsync();
				return stream.ToByte();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<bool> DeleteContainerAsync(string container)
		{
			try
			{
				var cloudBlobContainer = BlobClient.GetContainerReference(ParseContainerName(container));
				return await cloudBlobContainer.DeleteIfExistsAsync();
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
