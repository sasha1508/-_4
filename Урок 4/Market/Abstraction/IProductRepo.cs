using Market.DTO;

namespace Market
{
    public interface IProductRepo
    {
        public void AddProduct(ProductViewModel productViewModel);
        public IEnumerable<ProductViewModel> GetProducts();
        public string GetProductsCsv();
        public bool CheckProduct(int productId);
    }
}
