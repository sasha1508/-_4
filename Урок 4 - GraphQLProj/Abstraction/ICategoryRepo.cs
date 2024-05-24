using GraphQLProj.DTO;

namespace GraphQLProj.Abstraction
{
    public interface ICategoryRepo
    {
        public int AddCategory(CategoryViewModel productViewModel);
        public IEnumerable<CategoryViewModel> GetCategories();
    }
}
