using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSpike.Infrastructure.Dto
{   
    [Table("Image")]
    public class ImageDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        //public byte[] Data { get; set; }
    }
}
