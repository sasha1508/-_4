using Autofac.Core;
using MarketQL.Abstractions;
using MarketQL.DTO;
using MarketQL.Repo;
using MarketQL.Model;

namespace MarketQL.GraphQLServices.Mutation
{
    public class Mutation
    {
        private readonly IProductGroupRepo _productGroupRepo;
        private readonly IProductRepo _productRepo;
        private readonly IStorageRepo _storageRepo;
        private readonly IProductStorageRepo _productStorageRepo;
        public Mutation(IProductGroupRepo productGroupRepo, IProductRepo productRepo, IStorageRepo storageRepo, IProductStorageRepo productStorageRepo)
        {
            _productGroupRepo = productGroupRepo;
            _productRepo = productRepo;
            _storageRepo = storageRepo;
            _productStorageRepo = productStorageRepo;

        }

        public int AddProductGroup(ProductGroupViewModel productGroupViewModel)=> _productGroupRepo.AddProductGroup(productGroupViewModel);

        public int AddProduct(ProductViewModel productViewModel) => _productRepo.AddProduct(productViewModel);

        public int AddStorage(StorageViewModel storageViewModel) => _storageRepo.AddStorage(storageViewModel);

        public int AddProductToStorage(ProductStorageViewModel productStorageViewModel) => _productStorageRepo.AddProductToStorage(productStorageViewModel);
    }
}
