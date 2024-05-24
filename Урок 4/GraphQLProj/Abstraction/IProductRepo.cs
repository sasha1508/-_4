using GraphQLProj.DTO;

namespace GraphQLProj
{
    public interface IProductRepo
    {
        public int AddProduct(ProductViewModel productViewModel);
        public IEnumerable<ProductViewModel> GetProducts();
        public string GetProductsCsv();
    }
}
