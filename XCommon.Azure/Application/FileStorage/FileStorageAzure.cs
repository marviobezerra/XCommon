using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using XCommon.Application.FileStorage;
using System;
using System.Configuration;

namespace XCommon.Azure.Application.FileStorage
{
    public class FileStorageAzure : IFileStorage
    {
        private CloudStorageAccount Account { get; set; }
        private CloudBlobClient BlobClient { get; set; }

        public FileStorageAzure()
        {
            string cnx = ConfigurationManager.AppSettings["Prospect:BlobCnx"];
            
            Account = CloudStorageAccount.Parse(cnx);
            BlobClient = Account.CreateCloudBlobClient();            
        }

        public bool Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteIfExists(string fileName)
        {
            throw new NotImplementedException();
        }
        public bool DeleteIfExists(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string container, string fileName)
        {
            throw new NotImplementedException();
        }

        public byte[] Load(string fileName)
        {
            throw new NotImplementedException();
        }

        public byte[] Load(string container, string fileName)
        {
            try
            {
                CloudBlobContainer blobContainer = BlobClient.GetContainerReference(container);

                if (blobContainer.CreateIfNotExists())
                    blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                var blockBlob = blobContainer.GetBlockBlobReference(fileName);
                
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    blockBlob.DownloadToStream(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        public byte[] LoadIfExists(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Save(string fileName, byte[] content)
        {
            throw new Exception("You need container name to send file to Azure");            
        }

        public bool Save(string container, string fileName, byte[] content)
        {
            try
            {
                CloudBlobContainer blobContainer = BlobClient.GetContainerReference(container);

                if (blobContainer.CreateIfNotExists())
                    blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                
                var blockBlob = blobContainer.GetBlockBlobReference(fileName);
                blockBlob.UploadFromByteArray(content, 0, content.Length);

                return true;
            }
            catch
            {
                return false;
            }
        }        
    }
}
