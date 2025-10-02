namespace Domain.DTOs
{
    public class SupplierUpdateDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
    }
}