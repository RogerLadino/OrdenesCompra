using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;
using Shared.DTOs.Order;
using Domain.Exceptions;

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
        return orders.Adapt<IEnumerable<OrderDto>>();
    }

    public async Task<OrderDto> GetByIdAsync(int orderId)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
            throw new OrderNotFoundException("No order exists with given ID");

        return order.Adapt<OrderDto>();
    }

    public async Task<OrderDto?> CreateAsync(OrderCreationDto orderForCreationDto)
    {
        var order = orderForCreationDto.Adapt<Order>();

        var orderExists = await _repositoryManager.OrderRepository
            .AnyAsync(o => o.OrderNumber.Equals(order.OrderNumber) && o.Id != order.Id);

        if (orderExists)
            throw new OrderNumberAlreadyExistsException("Order number already exists");

        _repositoryManager.OrderRepository.Add(order);
        await _repositoryManager.SaveChangesAsync();

        return order.Adapt<OrderDto>();
    }

    public async Task UpdateAsync(int orderId, OrderDto orderForUpdateDto)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
            throw new OrderNotFoundException("No order exists with given ID");

        orderForUpdateDto.Adapt(order);

        var orderExists = await _repositoryManager.OrderRepository
            .AnyAsync(o => o.OrderNumber.Equals(order.OrderNumber) && o.Id != orderId);

        if (orderExists)
            throw new OrderNumberAlreadyExistsException("Order number already exists");

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int orderId)
    {
        var order = await _repositoryManager.OrderRepository.GetByIdAsync(orderId);

        if (order is null)
            throw new OrderNotFoundException("No order exists with given ID");

        _repositoryManager.OrderRepository.Remove(order);
        await _repositoryManager.SaveChangesAsync();
    }
}
