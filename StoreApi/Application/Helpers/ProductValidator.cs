using StoreApi.Application.Interfaces;
using StoreApi.Domain.Exceptions;

namespace StoreApi.Application.Helpers
{
    public static class ProductValidator
    {
        public static void ValidateProduct(IProductValidationModel model, bool requireAllFields = false)
        {
            if (requireAllFields)
            {
                if (!model.Price.HasValue)
                    throw new DomainValidationException("Price is required");
                if (!model.StockQuantity.HasValue)
                    throw new DomainValidationException("StockQuantity is required");
            }

            if (model.Price.HasValue && model.Price <= 0)
                throw new DomainValidationException("Price must be > 0");

            if (model.StockQuantity.HasValue && model.StockQuantity < 0)
                throw new DomainValidationException("Stock cannot be negative");
        }
    }
}
