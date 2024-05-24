using System.Text;
using AutoMapper;
using GraphQLProj.DB;
using GraphQLProj.DTO;
using GraphQLProj.Models;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQLProj.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        private IConfiguration _configuration;

        public ProductRepo(IMapper mapper, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public int AddProduct(ProductViewModel productViewModel)
        {
            using (ProductContext context = new ProductContext(_configuration.GetConnectionString("db")))
            {
                var entityProduct = context.Products.FirstOrDefault(x =>
                    x.Name.ToLower().Equals(productViewModel.Name.ToLower()));
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
                    throw new Exception("There is product's dublicate in database");
                }
            }

            return productViewModel.Id??0;
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out List<ProductViewModel> productsCashe))
            {
                return productsCashe;
            }

            using (ProductContext context = new ProductContext(_configuration.GetConnectionString("db")))
            {
                var products = context.Products.Select(_mapper.Map<ProductViewModel>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }

        public string GetProductsCsv()
        {
            StringBuilder sb = new StringBuilder();

            if (_memoryCache.TryGetValue("products", out List<ProductViewModel> productsCashe))
            {
                foreach (var productCash in productsCashe)
                {
                    sb.AppendLine(productCash.Name + ";" + productCash.Description + "\n");
                }

                return sb.ToString();
            }

            using (ProductContext context = new ProductContext(_configuration.GetConnectionString("db")))
            {
                var products = context.Products.Select(_mapper.Map<ProductViewModel>).ToList();

                foreach (var product in products)
                {
                    sb.AppendLine(product.Name + ";" + product.Description + "\n");
                }

                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
                return sb.ToString();
            }
        }
    }
}