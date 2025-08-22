using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int productId);
    void Insert(Product product);
    void Remove(Product product);
    Task<bool> NameExists(string name);
    Task<bool> IdExists(int id);
}