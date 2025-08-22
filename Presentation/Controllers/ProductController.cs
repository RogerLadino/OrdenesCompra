using Domain.DTOs;
using Service.Abstractions;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productForCreationDto)
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
}
