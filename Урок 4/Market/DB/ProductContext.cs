using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DB
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Category> Categories { get; set; }

        private string _connectionString;

        public ProductContext()
        {
        }

        public ProductContext(string connectionString)
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
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("product_pkey");

                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("description");

                entity.HasOne(e => e.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(e => e.CategoryId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("category_pkey");

                entity.ToTable("categories");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Models.Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("storage_pkey");

                entity.ToTable("storage");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Models.ProductStorage>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.StorageId }).HasName("product_storage_pkey");

                entity.HasOne(ps => ps.Storage)
                    .WithMany(s => s.Products)
                    .HasForeignKey(ps => ps.StorageId);
                entity.HasOne(ps => ps.Product)
                    .WithMany(s => s.Storages)
                    .HasForeignKey(ps => ps.ProductId);
            });

        }
    }
}