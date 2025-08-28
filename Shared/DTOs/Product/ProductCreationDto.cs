using Domain.Entities;

namespace Shared.DTOs.Product;
public class ProductCreationDto
{
    public string ProductName { get; set; } = null!;

    public int SupplierId { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Package { get; set; }

    public bool IsDiscontinued { get; set; }
}
