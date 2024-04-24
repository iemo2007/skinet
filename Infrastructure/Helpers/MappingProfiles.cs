using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace Infrastructure.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, GetProductDTO>()
                .ForMember(dest => dest.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();
        }
    }
}
