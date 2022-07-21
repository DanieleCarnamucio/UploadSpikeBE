using System.IO;

namespace UploadSpike.Models
{
    public class ImageRequestModel
    {
        public string Title { get; set; }
        public Stream Data { get; set; }
    }
}
