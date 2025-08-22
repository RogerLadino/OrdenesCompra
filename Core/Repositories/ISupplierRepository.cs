using Domain.Entities;

namespace Domain.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAllAsync();
    Task<Supplier?> GetByIdAsync(int supplierId);
    void Insert(Supplier supplier);
    void Remove(Supplier supplier);
    Task<bool> IdExists(int id);
    Task<bool> PhoneExists(string phone);
    Task<bool> EmailExists(string email);
    Task<bool> CompanyNameExists(string companyName);
}
