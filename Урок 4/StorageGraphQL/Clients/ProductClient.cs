
using StorageGraphQL.Abstraction;

namespace StorageGraphQL.Clients
{
    public class ProductClient : IProductClient
    {
        readonly HttpClient client = new HttpClient();
        public async Task<bool> Exists(int? id)
        {
            using HttpResponseMessage response = await client.GetAsync($"https://localhost:7159/Product/checkproduct?productId={id}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            if (responseBody == "true")
            {
                return true;
            }
            if (responseBody == "false") { return false; }

            throw new Exception("Unknow response");
        }
    }
}
