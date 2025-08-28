using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs.Customers;
using Shared.DTOs.Order;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CustomersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customersDto = await _serviceManager.CustomerService.GetAllAsync();
        return Ok(customersDto);
    }

    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        var customerDto = await _serviceManager.CustomerService.GetByIdAsync(customerId);
        if (customerDto == null)
        {
            return NotFound();
        }
        return Ok(customerDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreationDto customerForCreationDto)
    {
        var createdCustomer = await _serviceManager.CustomerService.CreateAsync(customerForCreationDto);
        return CreatedAtAction(nameof(GetCustomerById), new { customerId = createdCustomer.Id }, createdCustomer);
    }

    [HttpPut("{customerId:int}")]
    public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] CustomerDto customerForUpdateDto)
    {
        await _serviceManager.CustomerService.UpdateAsync(customerId, customerForUpdateDto);
        return NoContent();
    }

    [HttpDelete("{customerId:int}")]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        await _serviceManager.CustomerService.DeleteAsync(customerId);
        return NoContent();
    }

    [HttpPatch("{customerId:int}")]
    public async Task<IActionResult> PatchCustomer(int customerId, [FromBody] JsonPatchDocument<CustomerDto> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest();
        }

        var customerDto = await _serviceManager.CustomerService.GetByIdAsync(customerId);

        if (customerDto is null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(customerDto);

        await _serviceManager.CustomerService.UpdateAsync(customerId, customerDto);

        return NoContent();
    }
}