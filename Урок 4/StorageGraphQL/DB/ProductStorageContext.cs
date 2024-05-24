using Microsoft.EntityFrameworkCore;
using StorageGraphQL.Models;


namespace StorageGraphQL.DB
{
    public class ProductStorageContext : DbContext
    {
        public DbSet<ProductStorage> ProductStorages { get; set; }

        private string _connectionString;

        public ProductStorageContext()
        {
        }

        public ProductStorageContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseNpgsql(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductStorage>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.StorageId }).HasName("product_storage_pkey");

                entity.ToTable("productstorages");

                entity.Property(e => e.ProductId).HasColumnName("productid");
                entity.Property(e => e.StorageId).HasColumnName("storageid");
            });
            //OnModelCreating(modelBuilder);
        }
    }
}