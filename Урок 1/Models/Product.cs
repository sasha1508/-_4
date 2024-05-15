namespace GB_Market.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public int ? ProductGroupId { get; set; }
        public virtual List<ProductStorage> Storages { get; set; }
    }
}
