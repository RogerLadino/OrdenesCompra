using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly RepositoryDbContext _dbContext;

    public CustomerRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Customer>> GetAllAsync() =>
        await _dbContext.Customers.ToListAsync();

    public async Task<Customer?> GetByIdAsync(int customerId) =>
        await _dbContext.Customers.FindAsync(customerId);

    public void Insert(Customer customer) => _dbContext.Customers.Add(customer);

    public void Remove(Customer customer) => _dbContext.Customers.Remove(customer);

    public Task<bool> IdExists(int id) => _dbContext.Customers.AnyAsync(c => c.Id == id);

    public Task<bool> PhoneExists(string phone) => _dbContext.Customers.AnyAsync(c => c.Phone == phone);

    public Task<bool> EmailExists(string email) => _dbContext.Customers.AnyAsync(c => c.Email == email);
}