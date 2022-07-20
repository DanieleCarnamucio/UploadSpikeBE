using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadSpike.Infrastructure.Dto;

namespace UploadSpike.Infrastructure.Database
{
    public class UploadDbContext : DbContext 
    {
        public UploadDbContext(DbContextOptions options)  : base(options)
        {

        }

        public DbSet<ImageDto> Images { get; set; }
    }
}
