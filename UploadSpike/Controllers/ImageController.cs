using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using UploadSpike.Infrastructure.Database;
using UploadSpike.Infrastructure.Dto;
using UploadSpike.Infrastructure.Interfaces;
using UploadSpike.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UploadSpike.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageDao _imageDao;

        public ImageController(IImageDao imageDao)
        {
            _imageDao = imageDao;
        }

        // GET: api/<ImageController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_imageDao.Get());
        }

        // POST api/<ImageController>
        [HttpPost]
        public IActionResult Post(ImageRequestModel request)
        {
        //http://www.binaryintellect.net/articles/2f55345c-1fcb-4262-89f4-c4319f95c5bd.aspx
        //https://docs.microsoft.com/en-us/azure/event-grid/storage-upload-process-images?tabs=dotnet%2Cazure-powershell

            ImageDto img = new ImageDto();
                img.Title = request.TitleImage;
                img.Data = request.DataImage;
                //MemoryStream ms = new MemoryStream();
                //file.CopyTo(ms);
                //img.Data = ms.ToArray();

                //ms.Close();
                //ms.Dispose();

                _imageDao.Post(img);
                
            
            //ViewBag.Message = "Image(s) stored in database!";
            return Ok("tutto molto bello");
        }

        
    }
}
