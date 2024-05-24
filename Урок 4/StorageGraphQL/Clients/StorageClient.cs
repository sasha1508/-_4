using StorageGraphQL.Abstraction;

namespace StorageGraphQL.Clients
{
    public class StorageClient: IStorageClient
    {
        readonly HttpClient client = new HttpClient();
        public async Task<bool> Exists(int? id)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://localhost:7159/Storage/checkstorage?storageId={id}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (responseBody == "true")
            {
                return true;
            }
            if (responseBody == "false") { return false;}

            throw new Exception("Unknow response");
        }
    }
}
