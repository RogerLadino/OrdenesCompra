using System;
using System.Collections.Generic;

namespace Shared.DTOs.OrderItem;

public partial class OrderItemDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}
