using StoreApi.Application.DTOs;
using StoreApi.Application.DTOs.Requests;

namespace StoreApi.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> ListAllAsync();

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

    }
}