using Domain.Entities;
using Shared.DTOs.Order;

namespace Service.Abstractions;

public interface IOrderService : IServiceBase<OrderDto>
{
    Task<OrderDto> CreateAsync(OrderCreationDto order);
}