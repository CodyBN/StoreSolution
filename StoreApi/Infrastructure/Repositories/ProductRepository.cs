using Microsoft.EntityFrameworkCore;
using StoreApi.Application.DTOs;
using StoreApi.Application.Interfaces;
using StoreApi.Domain.Entities;

namespace StoreApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _db;
        public ProductRepository(StoreDbContext db) => _db = db;

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Product product)
        {
            product.IsActive = false;
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Product>> ListActiveWithCategoryAsync()
        {
            return await _db.Products.AsNoTracking()
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<PagedResult<Product>> SearchAsync(ProductSearchParams p)
        {
            var query = _db.Products.AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            if (p.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == p.CategoryId.Value);

            if (p.InStock.HasValue)
            {
                if (p.InStock.Value)
                    query = query.Where(x => x.StockQuantity > 0);
                else
                    query = query.Where(x => x.StockQuantity == 0);
            }

            if (p.MinPrice.HasValue)
                query = query.Where(x => x.Price >= p.MinPrice.Value);
            if (p.MaxPrice.HasValue)
                query = query.Where(x => x.Price <= p.MaxPrice.Value);

            // multi-word AND search across name and description (case-insensitive)
            if (!string.IsNullOrWhiteSpace(p.SearchTerm))
            {
                var words = p.SearchTerm.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(w => w.Trim().ToLowerInvariant())
                                        .ToArray();
                foreach (var w in words)
                {
                    query = query.Where(x =>
                        (x.Name != null && EF.Functions.Like(x.Name.ToLower(), $"%{w}%")) ||
                        (x.Description != null && EF.Functions.Like(x.Description.ToLower(), $"%{w}%"))
                    );
                }
            }

            // Sorting
            bool desc = string.Equals(p.SortOrder, "desc", StringComparison.OrdinalIgnoreCase);
            query = (p.SortBy?.ToLowerInvariant()) switch
            {
                "price" => desc ? query.OrderByDescending(x => x.Price) : query.OrderBy(x => x.Price),
                "name" => desc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
                "createddate" => desc ? query.OrderByDescending(x => x.CreatedDate) : query.OrderBy(x => x.CreatedDate),
                _ => query.OrderBy(x => x.Id)
            };

            // Paging
            var total = await query.CountAsync();
            var pageNumber = Math.Max(1, p.PageNumber);
            var pageSize = Math.Clamp(p.PageSize, 1, 100);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            //If you were dealing with a large database, you could consider getting just the Id's for the current page
            //Then retrieving entities including the categories. The current implementation joins then trims. 

            return new PagedResult<Product>(items, total, pageNumber, pageSize);
        }
    }

}
