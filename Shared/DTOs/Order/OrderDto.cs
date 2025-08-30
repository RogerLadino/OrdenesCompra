using Shared.DTOs.Customers;
using Shared.DTOs.OrderItem;
using System;
using System.Collections.Generic;

namespace Shared.DTOs.Order;

public partial class OrderDto
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string? OrderNumber { get; set; }

    public int CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}
