using Microsoft.EntityFrameworkCore;
using StoreApi.Domain.Entities;

namespace StoreApi.Infrastructure
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Price);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId);
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.IsActive);
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.IsActive);


            
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true },
                new Category { Id = 2, Name = "Books", Description = "Printed books and ebooks", IsActive = true },
                new Category { Id = 3, Name = "Clothing", Description = "Men and Women apparel", IsActive = true },
                new Category { Id = 4, Name = "Home", Description = "Home and kitchen", IsActive = true }
            );

            var fixedDate = new DateTime(2025, 11, 22, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<Product>().HasData(
                // Electronics (CategoryId = 1)
                new Product { Id = 1, Name = "Smartphone", Description = "Latest smartphone model", Price = 699.99m, CategoryId = 1, StockQuantity = 50, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 2, Name = "Laptop", Description = "Powerful laptop for work and play", Price = 1299.99m, CategoryId = 1, StockQuantity = 30, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 3, Name = "Wireless Headphones", Description = "Noise-cancelling over-ear headphones", Price = 199.99m, CategoryId = 1, StockQuantity = 80, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 4, Name = "Smartwatch", Description = "Fitness tracking smartwatch", Price = 179.50m, CategoryId = 1, StockQuantity = 120, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 5, Name = "Bluetooth Speaker", Description = "Portable bluetooth speaker", Price = 59.99m, CategoryId = 1, StockQuantity = 75, CreatedDate = fixedDate, IsActive = true },

                // Books (CategoryId = 2)
                new Product { Id = 6, Name = "Bestseller Novel", Description = "Bestselling fiction novel", Price = 19.99m, CategoryId = 2, StockQuantity = 100, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 7, Name = "Cookbook", Description = "Delicious recipes from around the world", Price = 24.99m, CategoryId = 2, StockQuantity = 60, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 8, Name = "C# Programming", Description = "Comprehensive C# programming guide", Price = 39.99m, CategoryId = 2, StockQuantity = 40, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 9, Name = "Children's Book", Description = "Illustrated storybook for kids", Price = 12.99m, CategoryId = 2, StockQuantity = 150, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 10, Name = "Mystery Thriller", Description = "Page-turning mystery thriller", Price = 17.50m, CategoryId = 2, StockQuantity = 85, CreatedDate = fixedDate, IsActive = true },

                // Clothing (CategoryId = 3)
                new Product { Id = 11, Name = "T-Shirt", Description = "Cotton T-Shirt", Price = 14.99m, CategoryId = 3, StockQuantity = 200, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 12, Name = "Jeans", Description = "Denim jeans", Price = 39.99m, CategoryId = 3, StockQuantity = 150, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 13, Name = "Jacket", Description = "Lightweight jacket", Price = 89.99m, CategoryId = 3, StockQuantity = 45, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 14, Name = "Sneakers", Description = "Comfortable everyday sneakers", Price = 69.99m, CategoryId = 3, StockQuantity = 90, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 15, Name = "Hoodie", Description = "Warm cotton hoodie", Price = 49.99m, CategoryId = 3, StockQuantity = 110, CreatedDate = fixedDate, IsActive = true },

                // Home (CategoryId = 4)
                new Product { Id = 16, Name = "Blender", Description = "Kitchen blender for smoothies", Price = 49.99m, CategoryId = 4, StockQuantity = 40, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 17, Name = "Toaster", Description = "2-slice toaster", Price = 29.99m, CategoryId = 4, StockQuantity = 65, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 18, Name = "Vacuum Cleaner", Description = "Compact vacuum cleaner", Price = 129.99m, CategoryId = 4, StockQuantity = 25, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 19, Name = "Coffee Maker", Description = "Automatic drip coffee maker", Price = 79.99m, CategoryId = 4, StockQuantity = 55, CreatedDate = fixedDate, IsActive = true },
                new Product { Id = 20, Name = "Pillow", Description = "Ergonomic memory foam pillow", Price = 34.99m, CategoryId = 4, StockQuantity = 140, CreatedDate = fixedDate, IsActive = true }
            );
        }
    }
}
