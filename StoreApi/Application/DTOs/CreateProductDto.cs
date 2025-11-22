using StoreApi.Application.Interfaces;
using StoreApi.Domain.Exceptions;

namespace StoreApi.Application.DTOs
{
    public class CreateProductDto : IProductValidationModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public int? StockQuantity { get; set; }

   
       
    }
}
