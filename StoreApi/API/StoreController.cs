using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using StoreApi.Application.DTOs;
using StoreApi.Application.DTOs.Requests;
using StoreApi.Application.Interfaces;
using StoreApi.Domain.Entities;
using StoreApi.Domain.Exceptions;
using StoreApi.Models;

namespace StoreApi.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {


        private readonly ILogger<StoreController> _logger;
        private readonly IProductService ProductService;
        private readonly ICategoryService CategoryService;
        public StoreController(ILogger<StoreController> logger, IProductService productService, ICategoryService categoryService)
        {

            _logger = logger;
            ProductService = productService;
            CategoryService = categoryService;
        }


        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var allProducts = await ProductService.ListAllAsync();
        
            return Ok(allProducts);
        }
        [HttpGet("products/{id}")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            var product = await ProductService.GetByIdAsync(id);
            return Ok(product);
        }
        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product)
        {
            var newProduct = await ProductService.CreateAsync(product);
            return CreatedAtAction(nameof(GetProductsById), new { id = newProduct.Id }, newProduct);
        }
        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto product, int id)
        {
            await ProductService.UpdateAsync(id,product);
            return Ok(product);
        }
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await ProductService.SoftDeleteAsync(id);
            return NoContent();
        }

        [HttpGet("products/search")]
        public async Task<IActionResult> SearchProducts([FromQuery] ProductSearchRequest request)
        {
            var result = await ProductService.SearchAsync(request);
            return Ok(result);
        }

        /**
         *  I kept everything in one controller for simplicity. You could separate these into different controllers if desired for more 
         *  decoupling and organization.
         * 
         */



        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await CategoryService.ListAllAsync();
            return Ok(result);
        }
        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto category)
        {
            var newProduct = await CategoryService.CreateAsync(category);
            return CreatedAtAction(nameof(GetProductsById), new { id = newProduct.Id }, newProduct);
        }

    }
}
