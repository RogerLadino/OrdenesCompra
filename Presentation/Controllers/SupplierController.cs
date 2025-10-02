using Domain.DTOs;
using Service.Abstractions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/suppliers")]
public class SuppliersController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public SuppliersController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var suppliersDto = await _serviceManager.SupplierService.GetAllAsync();
        return Ok(suppliersDto);
    }

    [HttpGet("{supplierId:int}")]
    public async Task<IActionResult> GetSupplierById(int supplierId)
    {
        var supplierDto = await _serviceManager.SupplierService.GetByIdAsync(supplierId);
        if (supplierDto == null)
        {
            return NotFound();
        }
        return Ok(supplierDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSupplier([FromBody] SupplierDto supplierForCreationDto)
    {
        var createdSupplier = await _serviceManager.SupplierService.CreateAsync(supplierForCreationDto);
        return CreatedAtAction(nameof(GetSupplierById), new { supplierId = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut("{supplierId:int}")]
    public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] SupplierUpdateDto supplierForUpdateDto)
    {
        var updated = await _serviceManager.SupplierService.UpdateAsync(supplierId, supplierForUpdateDto);
        if (updated == null)
            return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{supplierId:int}")]
    public async Task<IActionResult> DeleteSupplier(int supplierId)
    {
        await _serviceManager.SupplierService.DeleteAsync(supplierId);
        return NoContent();
    }
}
