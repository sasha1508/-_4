namespace StorageGraphQL.Abstraction;

public interface IStorageClient
{
    public Task<bool> Exists(int? id);
}