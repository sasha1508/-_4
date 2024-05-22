using AutoMapper;
using MarketQL.DTO;
using MarketQL.Model;

namespace MarketQL.Mapping
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
