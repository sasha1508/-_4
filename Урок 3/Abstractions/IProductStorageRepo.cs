using MarketQL.DTO;
using MarketQL.Model;

namespace MarketQL.Abstractions
{
    public interface IProductStorageRepo
    {
        public int AddProductToStorage(ProductStorageViewModel productStorageViewModel);
        public IEnumerable<StorageViewModel> GetStoragesInStorage();
    }
}
