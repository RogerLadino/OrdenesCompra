using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;
using Shared.DTOs.Order;

namespace Services;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;

    public OrderService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _repositoryManager.OrderRepository.GetAllAsync();
        var orderDtos = orders.Adapt<IEnumerable<OrderDto>>();
        return orderDtos;
    }

    public async Task<OrderDto?> GetByIdAsync(int orderId)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
            return null;

        return order.Adapt<OrderDto>();
    }

    public async Task<OrderDto?> CreateAsync(OrderCreationDto orderForCreationDto)
    {
        var order = orderForCreationDto.Adapt<Order>();

        var orderExists = await _repositoryManager.OrderRepository
            .AnyAsync(o => o.OrderNumber.Equals(order.OrderNumber));

        if (orderExists)
            return null;

        _repositoryManager.OrderRepository.Add(order);
        await _repositoryManager.SaveChangesAsync();

        return order.Adapt<OrderDto>();
    }

    public async Task UpdateAsync(int orderId, OrderDto orderForUpdateDto)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
            return;

        orderForUpdateDto.Adapt(order);

        var orderExists = await _repositoryManager.OrderRepository
            .AnyAsync(o => o.OrderNumber.Equals(order.OrderNumber) && o.Id != orderId);

        if (orderExists)
            return;

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
            return;

        _repositoryManager.OrderRepository.Remove(order);
        await _repositoryManager.SaveChangesAsync();
    }
}
