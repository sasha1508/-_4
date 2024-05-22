using MarketQL.DTO;

namespace MarketQL.Abstractions
{
    public interface IProductGroupRepo
    {

        public int AddProductGroup(ProductGroupViewModel productGroupViewModel);
        public IEnumerable<ProductGroupViewModel> GetProductGroups();
    }
}
