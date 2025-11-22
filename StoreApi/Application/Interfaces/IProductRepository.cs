using StoreApi.Application.DTOs;
using StoreApi.Domain.Entities;

namespace StoreApi.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task SoftDeleteAsync(Product product);
        Task<PagedResult<Product>> SearchAsync(ProductSearchParams searchParams);
        Task<IReadOnlyList<Product>> ListActiveWithCategoryAsync();
    }

}
