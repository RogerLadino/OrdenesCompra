using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs.Product;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ProductsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var productsDto = await _serviceManager.ProductService.GetAllAsync();
        return Ok(productsDto);
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var productDto = await _serviceManager.ProductService.GetByIdAsync(productId);
        if (productDto == null)
        {
            return NotFound();
        }
        return Ok(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreationDto productForCreationDto)
    {
        var createdProduct = await _serviceManager.ProductService.CreateAsync(productForCreationDto);
        return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{productId:int}")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductDto productForUpdateDto)
    {
        await _serviceManager.ProductService.UpdateAsync(productId, productForUpdateDto);
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        await _serviceManager.ProductService.DeleteAsync(productId);
        return NoContent();
    }

    [HttpPatch("{productId:int}")]
    public async Task<IActionResult> PatchProduct(int productId, [FromBody] JsonPatchDocument<ProductDto> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest();
        }

        var productDto = await _serviceManager.ProductService.GetByIdAsync(productId);

        if (productDto is null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(productDto);

        await _serviceManager.ProductService.UpdateAsync(productId, productDto);

        return NoContent();
    }
}
