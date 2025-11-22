using StoreApi.Application.DTOs;
using StoreApi.Application.DTOs.Requests;

namespace StoreApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> SearchAsync(ProductSearchRequest request);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(int id, UpdateProductDto dto);
        Task SoftDeleteAsync(int id);
        Task<IReadOnlyList<ProductDto>> ListAllAsync();
    }
}
