using AutoMapper;
using Market.DTO;
using Market.Models;

namespace Market.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Storage, StorageViewModel>().ReverseMap();
        }
    }
}
