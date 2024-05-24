using AutoMapper;
using GraphQLProj.DTO;
using GraphQLProj.Models;

namespace GraphQLProj.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
