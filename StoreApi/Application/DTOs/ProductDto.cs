namespace StoreApi.Application.DTOs
{
    public record ProductDto(int Id, string Name, string? Description, decimal Price, int CategoryId, string CategoryName, int StockQuantity, DateTime CreatedDate, bool IsActive);
}
