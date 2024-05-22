using AutoMapper;
using MarketQL.Abstractions;
using MarketQL.DTO;
using MarketQL.Model;
using Microsoft.Extensions.Caching.Memory;
using MarketQL.DB;
using MarketQL.Model;

namespace MarketQL.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        public ProductRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public int AddProduct(ProductViewModel productViewModel)
        {
            using (var context = new ProductContext())
            {

                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productViewModel.Name.ToLower()));
                if (entityProduct == null)
                {
                    var entity = _mapper.Map<Product>(productViewModel);
                    context.Products.Add(entity);
                    context.SaveChanges();
                    _memoryCache.Remove("products");
                    productViewModel.Id = entity.Id;
                }
                else
                {
                    throw new Exception("Product already exist");
                }
            }
            return productViewModel.Id??0;
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductViewModel> productsCache))
            {
                return productsCache;
            }
            using (var context = new ProductContext())
            {
                var products = context.Products.Select(_mapper.Map<ProductViewModel>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }
    }
}
