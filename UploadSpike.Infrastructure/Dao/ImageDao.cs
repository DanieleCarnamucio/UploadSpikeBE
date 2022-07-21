using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadSpike.Core.Interfaces;
using UploadSpike.Infrastructure.Database;
using UploadSpike.Infrastructure.Dto;
using UploadSpike.Infrastructure.Interfaces;

namespace UploadSpike.Infrastructure.Dao
{
    public class ImageDao : IImageDao
    {
        private readonly UploadDbContext _dbContext;
        private readonly AzureStorageConfig _storageConfig;

        public ImageDao(UploadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ImageDto Post(ImageDto obj)
        {
            obj.Id = 1;
            _dbContext.Images.Add(obj);
            _dbContext.SaveChanges();
            return obj;
        }

        public IEnumerable<ImageDto> Get()
        {
            return _dbContext.Images;
        }

        public  async Task<bool> UploadFileToStorage(Stream fileStream, string fileName,
                                                    AzureStorageConfig _storageConfig)
        {
            // Create a URI to the blob
            Uri blobUri = new Uri("https://" +
                                  _storageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  _storageConfig.ImageContainer +
                                  "/" + fileName);

            var p = blobUri.ToString();
            ImageDto imageDao = new ImageDto();
            imageDao.Path = p;
            _dbContext.Images.Add(imageDao);
            _dbContext.SaveChanges();

            // Create StorageSharedKeyCredentials object by reading
            // the values from the configuration (appsettings.json)
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            // Upload the file
            await blobClient.UploadAsync(fileStream);

            return await Task.FromResult(true);
        }
    }
}
