using MarketQL.DTO;
using MarketQL.Abstractions;


namespace MarketQL.Repo
{
    public class ProductGroupRepo : IProductGroupRepo
    {
        public int AddProductGroup(ProductGroupViewModel productGroupViewModel)
        {
            return 0;
        }

        public IEnumerable<ProductGroupViewModel> GetProductGroups()
        {
            throw new NotImplementedException();
        }
    }
}
