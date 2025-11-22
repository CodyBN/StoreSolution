using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic devices", true, "Electronics" },
                    { 2, "Printed books and ebooks", true, "Books" },
                    { 3, "Men and Women apparel", true, "Clothing" },
                    { 4, "Home and kitchen", true, "Home" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "IsActive", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Latest smartphone model", true, "Smartphone", 699.99m, 50 },
                    { 2, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Powerful laptop for work and play", true, "Laptop", 1299.99m, 30 },
                    { 3, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Noise-cancelling over-ear headphones", true, "Wireless Headphones", 199.99m, 80 },
                    { 4, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Fitness tracking smartwatch", true, "Smartwatch", 179.50m, 120 },
                    { 5, 1, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Portable bluetooth speaker", true, "Bluetooth Speaker", 59.99m, 75 },
                    { 6, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Bestselling fiction novel", true, "Bestseller Novel", 19.99m, 100 },
                    { 7, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Delicious recipes from around the world", true, "Cookbook", 24.99m, 60 },
                    { 8, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Comprehensive C# programming guide", true, "C# Programming", 39.99m, 40 },
                    { 9, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Illustrated storybook for kids", true, "Children's Book", 12.99m, 150 },
                    { 10, 2, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Page-turning mystery thriller", true, "Mystery Thriller", 17.50m, 85 },
                    { 11, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Cotton T-Shirt", true, "T-Shirt", 14.99m, 200 },
                    { 12, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Denim jeans", true, "Jeans", 39.99m, 150 },
                    { 13, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Lightweight jacket", true, "Jacket", 89.99m, 45 },
                    { 14, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Comfortable everyday sneakers", true, "Sneakers", 69.99m, 90 },
                    { 15, 3, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Warm cotton hoodie", true, "Hoodie", 49.99m, 110 },
                    { 16, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Kitchen blender for smoothies", true, "Blender", 49.99m, 40 },
                    { 17, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "2-slice toaster", true, "Toaster", 29.99m, 65 },
                    { 18, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Compact vacuum cleaner", true, "Vacuum Cleaner", 129.99m, 25 },
                    { 19, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Automatic drip coffee maker", true, "Coffee Maker", 79.99m, 55 },
                    { 20, 4, new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Ergonomic memory foam pillow", true, "Pillow", 34.99m, 140 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActive",
                table: "Categories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsActive",
                table: "Products",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Price",
                table: "Products",
                column: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
