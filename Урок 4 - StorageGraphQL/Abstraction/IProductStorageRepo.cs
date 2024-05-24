using StorageGraphQL.DTO;

namespace StorageGraphQL.Abstraction;

public interface IProductStorageRepo
{
    public int? AddProductStorage(ProductStorageViewModel productStorageViewModel);
    public void RemoveProductStorage(int? productId, int? storageId);
    public IEnumerable<int?> GetProductsIds(int? storageId);
}