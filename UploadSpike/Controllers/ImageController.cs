
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using UploadSpike.Infrastructure.Database;
using UploadSpike.Infrastructure.Dto;
using UploadSpike.Infrastructure.Interfaces;
using UploadSpike.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace UploadSpike.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageDao _imageDao;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ImageController(IImageDao imageDao, IMapper mapper, IConfiguration configuration)
        {
            _imageDao = imageDao;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost(nameof(UploadFile))]
        public async Task<IActionResult> UploadFile() 
        {
            IFormFile files = Request.Form.Files[0];

            string systemFileName = files.FileName;
            string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
            // Retrieve storage account from connection string.    
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
            // Create the blob client.    
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.    
            CloudBlobContainer container = blobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
            // This also does not make a service call; it only creates a local object.    
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
            await using (var data = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(data);
            }
            return Ok("File Uploaded Successfully");
        }


        [HttpPost(nameof(DownloadFile))]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            CloudBlockBlob blockBlob;
            await using (MemoryStream memoryStream = new MemoryStream())
            {
                string blobstorageconnection = _configuration.GetValue<string>("BlobConnectionString");
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(_configuration.GetValue<string>("BlobContainerName"));
                blockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }
            Stream blobStream = blockBlob.OpenReadAsync().Result;
            return File(blobStream, blockBlob.Properties.ContentType, blockBlob.Name);
        }


        //// GET: api/<ImageController>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_imageDao.Get());
        //}

        //// POST api/<ImageController>
        //[HttpPost]
        //public IActionResult Post(ImageRequestModel request)
        //{
        ////http://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
        ////https://docs.microsoft.com/en-us/azure/event-grid/storage-upload-process-images?tabs=dotnet%2Cazure-powershell

        //    var requestDto = _mapper.Map<ImageRequestModel, ImageDto>(request);
        //    //ImageDto img = new ImageDto();
        //        //img.Title = request.Title;
        //        //img.Data = request.Data;
        //        //MemoryStream ms = new MemoryStream();
        //        //file.CopyTo(ms);
        //        //img.Data = ms.ToArray();

        //        //ms.Close();
        //        //ms.Dispose();

        //        _imageDao.Post(requestDto);


        //    //ViewBag.Message = "Image(s) stored in database!";
        //    return Ok("tutto molto bello");
        //}


    }
}
