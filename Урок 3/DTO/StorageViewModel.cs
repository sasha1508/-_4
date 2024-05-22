using MarketQL.Model;


namespace MarketQL.DTO
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<ProductStorageViewModel>? Products { get; set; }

    }
}