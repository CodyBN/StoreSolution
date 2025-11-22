using StoreApi.Application.DTOs;
using StoreApi.Application.DTOs.Requests;
using StoreApi.Application.Helpers;
using StoreApi.Application.Interfaces;
using StoreApi.Domain.Entities;
using StoreApi.Domain.Exceptions;

namespace StoreApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResult<ProductDto>> SearchAsync(ProductSearchRequest request)
        {
            var searchParams = new ProductSearchParams
            {
                SearchTerm = request.SearchTerm,
                CategoryId = request.CategoryId,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice,
                InStock = request.InStock,
                SortBy = request.SortBy,
                SortOrder = request.SortOrder,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var result = await _repo.SearchAsync(searchParams);
            var items = result.Items.Select(MapToDto).ToList();
            return new PagedResult<ProductDto>(items, result.TotalCount, result.PageNumber, result.PageSize);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product is null ? throw new NotFoundException("Product Id not found.") : MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            ProductValidator.ValidateProduct(dto, requireAllFields: true);

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price!.Value ,
                CategoryId = dto.CategoryId,
                StockQuantity = dto.StockQuantity!.Value,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await _repo.AddAsync(product);
            return MapToDto(product);
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);

            //Quick validation
            if (existing is null || !existing.IsActive) throw new NotFoundException($"Product {id} not found");
            ProductValidator.ValidateProduct(dto, requireAllFields: false);

            //Update fields if provided
            if (!string.IsNullOrWhiteSpace(dto.Name)) existing.Name = dto.Name;
            if (dto.Description != null) existing.Description = dto.Description;
            if (dto.Price.HasValue) existing.Price = dto.Price.Value;
            if (dto.CategoryId.HasValue) existing.CategoryId = dto.CategoryId.Value;
            if (dto.StockQuantity.HasValue) existing.StockQuantity = dto.StockQuantity.Value;

            await _repo.UpdateAsync(existing);
        }

        public async Task SoftDeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) throw new NotFoundException($"Product {id} not found");

            await _repo.SoftDeleteAsync(existing);
        }

        public async Task<IReadOnlyList<ProductDto>> ListAllAsync()
        {
            var products = await _repo.ListActiveWithCategoryAsync();
            return products.Select(MapToDto).ToList();
        }

        // --- Manual mapping method ---
        private ProductDto MapToDto(Product product)
        {
            return new ProductDto(
                Id: product.Id,
                Name: product.Name,
                Description: product.Description,
                Price: product.Price,
                CategoryId: product.CategoryId,
                CategoryName: product.Category?.Name ?? "",
                StockQuantity: product.StockQuantity,
                CreatedDate: product.CreatedDate,
                IsActive: product.IsActive
            );
        }
    }
}
