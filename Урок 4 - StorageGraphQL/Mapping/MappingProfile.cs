using AutoMapper;
using StorageGraphQL.DTO;
using StorageGraphQL.Models;

namespace Market.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<ProductStorage, ProductStorageViewModel>().ReverseMap();
    }
}