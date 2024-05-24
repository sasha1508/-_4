using AutoMapper;
using Storage.DTO;
using Storage.Models;

namespace Market.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<ProductStorage, ProductStorageViewModel>().ReverseMap();
    }
}