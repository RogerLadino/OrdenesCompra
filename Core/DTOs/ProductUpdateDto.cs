namespace Domain.DTOs
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}