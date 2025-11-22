using System.Linq;
using StoreApi.Application.DTOs;
using StoreApi.Application.Interfaces;
using StoreApi.Domain.Entities;
using StoreApi.Domain.Exceptions;

namespace StoreApi.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<CategoryDto>> ListAllAsync()
        {
            var categories = await _repo.ListActiveAsync();
            return categories.Select(MapToDto).ToList();
        }

     
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new DomainValidationException("Name is required");

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                IsActive = true
            };

            await _repo.AddAsync(category);
            return MapToDto(category);
        }

        

        private static CategoryDto MapToDto(Category c)
            => new CategoryDto(c.Id, c.Name, c.Description, c.IsActive);
    }
}