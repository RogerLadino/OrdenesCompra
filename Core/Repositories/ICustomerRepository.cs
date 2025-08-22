using Domain.Entities;

namespace Domain.Repositories;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(int customerId);
    void Insert(Customer customer);
    void Remove(Customer customer);
    Task<bool> IdExists(int id);
    Task<bool> PhoneExists(string phone);
    Task<bool> EmailExists(string email);
}