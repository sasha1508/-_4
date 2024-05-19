using Market.DTO;

namespace Market.Abstractions
{
    public interface IProductGroupRepo
    {

        public void AddProductGroup(ProductGroupViewModel productGroupViewModel);
        public IEnumerable<ProductGroupViewModel> GetProductGroups();
    }
}
