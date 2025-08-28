using System;
using System.Collections.Generic;

namespace Shared.DTOs.OrderItem;

public partial class OrderItemCreationDto
{
    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}
