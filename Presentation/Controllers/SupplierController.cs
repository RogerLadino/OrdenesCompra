using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs.Supplier;

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
    public async Task<IActionResult> CreateSupplier([FromBody] SupplierCreationDto supplierForCreationDto)
    {
        var createdSupplier = await _serviceManager.SupplierService.CreateAsync(supplierForCreationDto);
        return CreatedAtAction(nameof(GetSupplierById), new { supplierId = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut("{supplierId:int}")]
    public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] SupplierDto supplierForUpdateDto)
    {
        await _serviceManager.SupplierService.UpdateAsync(supplierId, supplierForUpdateDto);
        return NoContent();
    }

    [HttpDelete("{supplierId:int}")]
    public async Task<IActionResult> DeleteSupplier(int supplierId)
    {
        await _serviceManager.SupplierService.DeleteAsync(supplierId);
        return NoContent();
    }

    [HttpPatch("{supplierId:int}")]
    public async Task<IActionResult> PatchSupplier(int supplierId, [FromBody] JsonPatchDocument<SupplierDto> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest();
        }

        var supplierDto = await _serviceManager.SupplierService.GetByIdAsync(supplierId);

        if (supplierDto is null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(supplierDto);

        await _serviceManager.SupplierService.UpdateAsync(supplierId, supplierDto);

        return NoContent();
    }
}
