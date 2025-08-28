using Shared.DTOs.Product;

namespace Service.Abstractions;

public interface IProductService : IServiceBase<ProductDto>
{
    Task<ProductDto> CreateAsync(ProductCreationDto product);
}