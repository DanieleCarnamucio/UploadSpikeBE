using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
    }
}
