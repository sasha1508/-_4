using GB_Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;

namespace GB_Market.DB
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Models.Storage> Storages { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql("Host=localhost;Port=5432;Username=test;Password=Test1234;Database=Market");
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

                entity.HasOne(e => e.ProductGroup).WithMany(p => p.Products).HasForeignKey(e=>e.ProductGroupId);
            });
            
            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("productgroup_pkey");

                entity.ToTable("productgroups");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
            });
            modelBuilder.Entity<Models.Storage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("storage_pkey");

                entity.ToTable("storage");

                entity.Property(e => e.Id).HasColumnName("id");

            });
            modelBuilder.Entity<ProductStorage>(entity =>
            {
            entity.HasKey(e => new {e.ProductId,e.StorageId}).HasName("product_storage_pkey");

                entity.HasOne(ps=>ps.Storage).WithMany(s=>s.Products).HasForeignKey(ps=>ps.StorageId);
                entity.HasOne(ps => ps.Product).WithMany(s => s.Storages).HasForeignKey(ps => ps.ProductId);
            });
        }

    }
}
