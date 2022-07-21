using AutoMapper;
using UploadSpike.Infrastructure.Dto;
using UploadSpike.Models;

namespace UploadSpike.Mapper
{
    public class UiProfile : Profile
    {
        public UiProfile()
        {
            CreateMap<ImageRequestModel, ImageDto>().ReverseMap();
        }
    }
}
