using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;
using Shared.DTOs.Product;
using Domain.Exceptions;

namespace Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repositoryManager.ProductRepository.GetAllAsync();
        return products.Adapt<IEnumerable<ProductDto>>();
    }

    public async Task<ProductDto?> GetByIdAsync(int productId)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId);

        if(product is null)
        {
            throw new ProductNotFoundException("No product exists with given ID");
        }

        return product?.Adapt<ProductDto>();
    }

    public async Task<ProductDto> CreateAsync(ProductCreationDto productForCreationDto)
    {
        var product = productForCreationDto.Adapt<Product>();

        var nameExists = await _repositoryManager.ProductRepository
            .AnyAsync(p => p.ProductName.Equals(product.ProductName));

        if (nameExists)
            throw new ProductNameAlreadyExistsException("Product name is already used");

        _repositoryManager.ProductRepository.Add(product);
        await _repositoryManager.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }

    public async Task UpdateAsync(int productId, ProductDto productForUpdateDto)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId);

        if (product is null)
            throw new ProductNotFoundException("No product exists with given ID");

        productForUpdateDto.Adapt(product);

        var nameExists = await _repositoryManager.ProductRepository
            .AnyAsync(p => p.ProductName.Equals(product.ProductName) && p.Id != productId);

        if (nameExists)
            throw new ProductNameAlreadyExistsException("Product name is already used");

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId);

        if (product is null)
            throw new ProductNotFoundException("No product exists with given ID");

        _repositoryManager.ProductRepository.Remove(product);
        await _repositoryManager.SaveChangesAsync();
    }
}
