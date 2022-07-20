using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadSpike.Core.Interfaces;
using UploadSpike.Infrastructure.Dto;

namespace UploadSpike.Infrastructure.Interfaces
{
    public interface IImageDao : ICreate<ImageDto>, IGet<IEnumerable<ImageDto>>
    {
    }
}
