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

        public string Post(string path)
        {
            var img = new ImageDto();
            img.Path = "https://acadesteamfilestorage.blob.core.windows.net/filecontainer/" + path;

            _dbContext.Images.Add(img);
            _dbContext.SaveChanges();
            return img.Path;
        }

        public IEnumerable<ImageDto> Get()
        {
            return _dbContext.Images;
        }

        public IEnumerable<ImageDto> GetBy(string param)
        {
            var path = "https://acadesteamfilestorage.blob.core.windows.net/filecontainer/" + param + ".png";
            var img = _dbContext.Images.Where(i => i.Path == path).ToList();

            return img;          
        }

        //public  async Task<bool> UploadFileToStorage(Stream fileStream, string fileName,
        //                                            AzureStorageConfig _storageConfig)
        //{
        //    // Create a URI to the blob
        //    Uri blobUri = new Uri("https://" +
        //                          _storageConfig.AccountName +
        //                          ".blob.core.windows.net/" +
        //                          _storageConfig.ImageContainer +
        //                          "/" + fileName);

        //    var p = blobUri.ToString();
        //    ImageDto imageDao = new ImageDto();
        //    imageDao.Path = p;
        //    _dbContext.Images.Add(imageDao);
        //    _dbContext.SaveChanges();

        //    // Create StorageSharedKeyCredentials object by reading
        //    // the values from the configuration (appsettings.json)
        //    StorageSharedKeyCredential storageCredentials =
        //        new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

        //    // Create the blob client.
        //    BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

        //    // Upload the file
        //    await blobClient.UploadAsync(fileStream);

        //    return await Task.FromResult(true);
        //}
    }
}
