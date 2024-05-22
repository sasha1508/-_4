using AutoMapper;
using MarketQL.Abstractions;
using MarketQL.DB;
using MarketQL.DTO;
using MarketQL.Model;
using Microsoft.Extensions.Caching.Memory;

namespace MarketQL.Repo
{
    public class ProductStorageRepo : IProductStorageRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        public ProductStorageRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddProductToStorage(ProductStorageViewModel productStorageViewModel)
        {
            using (var context = new ProductContext())
            {

                var entityProduct = context.Products.FirstOrDefault(x => x.Id == productStorageViewModel.ProductId);
                var entityStorage = context.Storages.FirstOrDefault(x => x.Id == productStorageViewModel.StorageId);
 
                productStorageViewModel.Product = entityProduct;
                productStorageViewModel.Storage = entityStorage;

                var entity = _mapper.Map<ProductStorage>(productStorageViewModel);
                context.ProductStorage.Add(entity);
                context.SaveChanges();
                _memoryCache.Remove("productsStorage");

            }
            return productStorageViewModel.Count;
        }

        public IEnumerable<StorageViewModel> GetStoragesInStorage()
        {
            throw new NotImplementedException();
        }
    }
}
