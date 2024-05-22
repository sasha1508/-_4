using MarketQL.DTO;

namespace MarketQL.Abstractions
{
    public interface IProductRepo
    {
        public int AddProduct(ProductViewModel productViewModel);
        public IEnumerable<ProductViewModel> GetProducts();


    }
}
