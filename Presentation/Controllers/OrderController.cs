using Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs.Order;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public OrdersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var ordersDto = await _serviceManager.OrderService.GetAllAsync();
        return Ok(ordersDto);
    }

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var orderDto = await _serviceManager.OrderService.GetByIdAsync(orderId);
        if (orderDto == null)
            return NotFound();

        return Ok(orderDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreationDto orderForCreationDto)
    {
        var createdOrder = await _serviceManager.OrderService.CreateAsync(orderForCreationDto);
        if (createdOrder == null)
            return Conflict("El número de orden ya existe.");

        return CreatedAtAction(
            nameof(GetOrderById),
            new { orderId = createdOrder.Id },
            createdOrder
        );
    }

    [HttpPut("{orderId:int}")]
    public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] OrderDto orderForUpdateDto)
    {
        await _serviceManager.OrderService.UpdateAsync(orderId, orderForUpdateDto);
        return NoContent();
    }

    [HttpDelete("{orderId:int}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        await _serviceManager.OrderService.DeleteAsync(orderId);
        return NoContent();
    }

    [HttpPatch("{orderId:int}")]
    public async Task<IActionResult> PatchOrder(int orderId, [FromBody] JsonPatchDocument<OrderDto> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest();
        }

        var orderDto = await _serviceManager.OrderService.GetByIdAsync(orderId);

        if (orderDto is null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(orderDto);

        await _serviceManager.OrderService.UpdateAsync(orderId, orderDto);

        return NoContent();
    }
}
