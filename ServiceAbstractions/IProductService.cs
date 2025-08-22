using Domain.DTOs;

namespace Service.Abstractions;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(int productId);

    Task<ProductDto> CreateAsync(ProductDto productForCreationDto);

    Task UpdateAsync(int productId, ProductDto productForUpdateDto);

    Task DeleteAsync(int productId);
}