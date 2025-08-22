using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly RepositoryDbContext _dbContext;

    public ProductRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Product>> GetAllAsync() =>
        await _dbContext.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int productId) =>
        await _dbContext.Products.FindAsync(productId);

    public void Insert(Product product) => _dbContext.Products.Add(product);

    public void Remove(Product product) => _dbContext.Products.Remove(product);

    public Task<bool> IdExists(int id) =>
        _dbContext.Products.AnyAsync(p => p.Id == id);

    public Task<bool> NameExists(string productName) =>
        _dbContext.Products.AnyAsync(p => p.ProductName == productName);
}
