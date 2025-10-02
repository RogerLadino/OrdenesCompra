using Domain.DTOs;

namespace Service.Abstractions;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(int productId);

    Task<ProductDto> CreateAsync(ProductDto productForCreationDto);

    Task<ProductDto?> UpdateAsync(int productId, ProductUpdateDto productForUpdateDto);

    Task DeleteAsync(int productId);
}