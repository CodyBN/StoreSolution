using StoreApi.Domain.Entities;

namespace StoreApi.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task SoftDeleteAsync(Category category);
        Task<IReadOnlyList<Category>> ListActiveAsync();
    }
}