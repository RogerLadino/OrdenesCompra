using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;

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

        var productDtos = products.Adapt<IEnumerable<ProductDto>>();

        return productDtos;
    }

    public async Task<ProductDto?> GetByIdAsync(int productId)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId);

        if (product is null)
        {
            return null;
        }

        var productDto = product.Adapt<ProductDto>();

        return productDto;
    }

    public async Task<ProductDto?> CreateAsync(ProductDto productForCreationDto)
    {
        var product = productForCreationDto.Adapt<Product>();

        if (await _repositoryManager.ProductRepository.IdExists(product.Id))
        {
            return null;
        }

        if (await _repositoryManager.ProductRepository.NameExists(product.ProductName))
        {
            return null;
        }

        _repositoryManager.ProductRepository.Insert(product);

        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }

    public async Task<ProductDto?> UpdateAsync(int productId, ProductUpdateDto productForUpdateDto)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return null;
        }

        product.ProductName = productForUpdateDto.ProductName;
        product.SupplierId = productForUpdateDto.SupplierId;
        product.UnitPrice = productForUpdateDto.UnitPrice;
        product.Package = productForUpdateDto.Package;
        product.IsDiscontinued = productForUpdateDto.IsDiscontinued;

        var existsWithName = (await _repositoryManager.ProductRepository.GetAllAsync())
            .Any(p => p.ProductName == product.ProductName && p.Id != productId);
        if (existsWithName)
        {
            return null;
        }

        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return product.Adapt<ProductDto>();
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await _repositoryManager.ProductRepository.GetByIdAsync(productId);

        if (product is null)
        {
            return;
        }

        _repositoryManager.ProductRepository.Remove(product);

        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }
}
