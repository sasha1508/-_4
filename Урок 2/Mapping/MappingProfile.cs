using AutoMapper;
using Market.DTO;
using Market.Model;

namespace Market.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupViewModel>().ReverseMap();
        }
    }
}
