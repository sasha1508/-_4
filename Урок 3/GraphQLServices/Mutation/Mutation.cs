using Autofac.Core;
using MarketQL.Abstractions;
using MarketQL.DTO;
using MarketQL.Repo;

namespace MarketQL.GraphQLServices.Mutation
{
    public class Mutation
    {
        private readonly IProductGroupRepo _productGroupRepo;
        private readonly IProductRepo _productRepo;
        public Mutation(IProductGroupRepo productGroupRepo, IProductRepo productRepo)
        {
            _productGroupRepo = productGroupRepo;
            _productRepo = productRepo;
        }

        public int AddProductGroup(ProductGroupViewModel productGroupViewModel)=> _productGroupRepo.AddProductGroup(productGroupViewModel);

        public int AddProduct(ProductViewModel productViewModel) => _productRepo.AddProduct(productViewModel);

    }
}
