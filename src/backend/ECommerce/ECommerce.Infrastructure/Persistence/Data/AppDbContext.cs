using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.AvatarUrl).HasMaxLength(500);
                entity.Property(e => e.RefreshToken).HasMaxLength(500);

                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasMany(e => e.Addresses)
                      .WithOne(a => a.User)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReceiverName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ReceiverPhone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.AddressLine).IsRequired().HasMaxLength(500);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Status).HasConversion<string>();

                entity.HasOne(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(p => p.CategoryId)
                        .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.Variants)
                        .WithOne(v => v.Product)
                        .HasForeignKey(v => v.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Images)
                        .WithOne(i => i.Product)
                        .HasForeignKey(i => i.ProductId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SKU).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Size).HasMaxLength(20);
                entity.Property(e => e.Color).HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                entity.HasIndex(e => e.SKU).IsUnique();
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.PublicId).HasMaxLength(255);

                entity.HasOne(i => i.Product)
                      .WithMany(p => p.Images)
                      .HasForeignKey(i => i.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(i => i.PublicId);
            });

            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Role = UserRole.Admin,
                FullName = "Admin",
                Email = "admin@datn.com",
                PasswordHash = "$2a$11$E3RGRhjfkGzTz5J42JIOXe3dpiCEGaiZZxLIYfm0qdwnc/xFU/w.u",
                CreatedAt = new DateTime(2026, 8, 20),
                UpdatedAt = new DateTime(2026, 8, 20),
                IsDeleted = false
            });
        }
    }
}
