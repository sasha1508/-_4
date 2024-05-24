using GraphQLProj.Abstraction;
using GraphQLProj.DTO;

namespace GraphQLProj.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        public int AddCategory(CategoryViewModel productViewModel)
        {
            return 0;
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            throw new NotImplementedException();
        }
    }
}
