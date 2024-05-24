﻿using DTO;
using Microsoft.EntityFrameworkCore;
using SecurityMarket.Model;

namespace SecurityMarket.UserContext
{
    public class UserRoleContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        private string _connectionString;

        public UserRoleContext(DbContextOptions<UserRoleContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public UserRoleContext()
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ent =>
            {
                ent.HasKey(x => x.Id).HasName("users_key");
                ent.HasIndex(x => x.Email).IsUnique();

                ent.ToTable("users");

                ent.Property(e => e.Id).HasColumnName("id");
                ent.Property(e => e.Email).HasMaxLength(255).HasColumnName("name");
                ent.Property(e => e.Password).HasMaxLength(255).HasColumnName("password");
                ent.Property(e => e.Salt).HasColumnName("salt");
                //ent.Property(e => e.RoleId).HasConversion<int>();

                ent.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            });

            modelBuilder.Entity<Role>().Property(e => e.RoleId).HasConversion<int>();

            modelBuilder.Entity<Role>().HasData(Enum.GetValues(typeof(UserRoleType)).Cast<UserRoleType>().Select(u =>
                new Role()
                {
                    RoleId = u,
                    Name = u.ToString()
                }));
        }
    }
}