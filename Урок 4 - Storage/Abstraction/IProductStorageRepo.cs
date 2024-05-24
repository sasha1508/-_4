using Storage.DTO;

namespace Storage.Abstraction;

public interface IProductStorageRepo
{
    public void AddProductStorage(ProductStorageViewModel productStorageViewModel);
    public void RemoveProductStorage(int? productId, int? storageId);
    public IEnumerable<int?> GetProductsIds(int? storageId);
}