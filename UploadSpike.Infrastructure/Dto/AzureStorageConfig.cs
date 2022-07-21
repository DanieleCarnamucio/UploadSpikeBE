using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSpike.Infrastructure.Dto
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string ImageContainer { get; set; }
        public string AccountKey { get; set; }
    }
}
