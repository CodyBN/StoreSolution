using Microsoft.EntityFrameworkCore;
using StoreApi.Application.Interfaces;
using StoreApi.Domain.Entities;

namespace StoreApi.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _db;
        public CategoryRepository(StoreDbContext db) => _db = db;

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories
                .AsNoTracking()
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
        }

        public async Task AddAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Category category)
        {
            category.IsActive = false;
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Category>> ListActiveAsync()
        {
            return await _db.Categories
                .AsNoTracking()
                .Where(c => c.IsActive)
                .ToListAsync();
        }
    }
}