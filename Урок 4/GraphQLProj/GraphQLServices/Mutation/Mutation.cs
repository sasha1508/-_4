using GraphQLProj.Abstraction;
using GraphQLProj.DTO;
using GraphQLProj.Repo;

namespace GraphQLProj.GraphQLServices.Mutation
{
    public class Mutation(ICategoryRepo categoryRepo, IProductRepo productRepo)
    {
        public int AddCategory(CategoryViewModel categoryViewModel) => categoryRepo.AddCategory(categoryViewModel);

        public int AddProduct(ProductViewModel productViewModel) => productRepo.AddProduct(productViewModel);
    }
}