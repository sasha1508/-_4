using Market.DTO;

namespace Market.Abstractions
{
    public interface IProductRepo
    {
        public void AddProduct(ProductViewModel productViewModel);
        public IEnumerable<ProductViewModel> GetProducts();

    }
}
