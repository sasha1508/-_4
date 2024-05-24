using GraphQLProj.DTO;
using GraphQLProj.Repo;

namespace GraphQLProj.GraphQLServices.Query
{
    public class Query
    {
        public IEnumerable<ProductViewModel> GetProduct([Service] ProductRepo productRepo) => productRepo.GetProducts();

    }
}