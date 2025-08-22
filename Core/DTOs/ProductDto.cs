using Domain.Entities;

namespace Domain.DTOs;
public class ProductDto
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int SupplierId { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Package { get; set; }

    public bool IsDiscontinued { get; set; }

    public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    public virtual SupplierDto? Supplier { get; set; } = null!;
}
