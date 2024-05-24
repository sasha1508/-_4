using Market.DTO;

namespace Market.Abstraction
{
    public interface ICategoryRepo
    {
        public void AddCategory(CategoryViewModel productViewModel);
        public IEnumerable<CategoryViewModel> GetCategories();
    }
}
