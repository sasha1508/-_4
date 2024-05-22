using MarketQL.DTO;

namespace MarketQL.Abstractions
{
    public interface IStorageRepo
    {
        public int AddStorage(StorageViewModel storageViewModel);
        public IEnumerable<StorageViewModel> GetStorages();
    }
}
