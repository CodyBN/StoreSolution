using StoreApi.Domain.Exceptions;

namespace StoreApi.Application.Interfaces
{
    public interface IProductValidationModel
    {
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
       
    }
}
