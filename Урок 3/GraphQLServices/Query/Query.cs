using MarketQL.Abstractions;
using MarketQL.DTO;
using MarketQL.Repo;

namespace MarketQL.GraphQLServices.Query
{
    public class Query
    {
        public IEnumerable<ProductViewModel> GetProducts([Service] ProductRepo productRepo) => productRepo.GetProducts();
        public IEnumerable<StorageViewModel> GetStorages([Service] StorageRepo storageRepo) => storageRepo.GetStorages();
    }
}
