using StorageGraphQL.Abstraction;
using StorageGraphQL.DTO;
using StorageGraphQL.Repo;

namespace StorageGraphQL.GraphQLServices.Mutation
{
    public class Mutation(ProductStorageRepo productStorageRepo)
    {
        public int? AddProductStorage(ProductStorageViewModel productStorageViewModel) => productStorageRepo.AddProductStorage(productStorageViewModel);

    }
}