using System.Text;
using AutoMapper;
using Market.DB;
using Market.DTO;
using Market.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Market.Repo
{
    public class ProductRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext context) : IProductRepo
    {
        public void AddProduct(ProductViewModel productViewModel)
        {
            var entityProduct = context.Products.FirstOrDefault(x =>
                x.Name.ToLower().Equals(productViewModel.Name.ToLower()));
            if (entityProduct == null)
            {
                var entity = mapper.Map<Product>(productViewModel);
                context.Products.Add(entity);
                context.SaveChanges();
                memoryCache.Remove("products");
            }
            else
            {
                throw new Exception("There is product's dublicate in database");
            }
        }

        public bool CheckProduct(int productId)
        {
            return context.Products.Any(x => x.Id == productId);
        }

        public IEnumerable<ProductViewModel> GetProducts()
        {
            if (memoryCache.TryGetValue("products", out List<ProductViewModel> productsCashe))
            {
                return productsCashe;
            }

            var products = context.Products.Select(mapper.Map<ProductViewModel>).ToList();
            memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
            return products;
        }

        public string GetProductsCsv()
        {
            StringBuilder sb = new StringBuilder();

            if (memoryCache.TryGetValue("products", out List<ProductViewModel> productsCashe))
            {
                foreach (var productCash in productsCashe)
                {
                    sb.AppendLine(productCash.Name + ";" + productCash.Description + "\n");
                }

                return sb.ToString();
            }


            var products = context.Products.Select(mapper.Map<ProductViewModel>).ToList();

            foreach (var product in products)
            {
                sb.AppendLine(product.Name + ";" + product.Description + "\n");
            }

            memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
            return sb.ToString();
        }
    }
}