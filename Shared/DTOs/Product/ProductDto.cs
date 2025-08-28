using Domain.Entities;

namespace Shared.DTOs.Product;
public class ProductDto
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int SupplierId { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Package { get; set; }

    public bool IsDiscontinued { get; set; }
}
