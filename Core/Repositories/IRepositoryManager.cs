using Domain.Repositories;

namespace Domain.Repositories;

public interface IRepositoryManager
{
    ICustomerRepository CustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    ISupplierRepository SupplierRepository { get; }
    Task SaveChangesAsync();
}