namespace StoreApi.Application.DTOs
{
    public class ProductSearchParams
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? InStock { get; set; }
        public string? SortBy { get; set; } = "id";     
        public string? SortOrder { get; set; } = "asc";  
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
