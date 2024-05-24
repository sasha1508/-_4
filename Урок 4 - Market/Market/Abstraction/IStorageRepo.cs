using Market.DTO;

namespace Market.Abstraction
{
    public interface IStorageRepo
    {
        public void AddStorage(StorageViewModel storageViewModel);
        public IEnumerable<StorageViewModel> GetStorages();
        public bool CheckStorage(int storageId);
    }
}
