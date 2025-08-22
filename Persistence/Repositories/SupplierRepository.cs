using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public sealed class SupplierRepository : ISupplierRepository
{
    private readonly RepositoryDbContext _dbContext;

    public SupplierRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

    public async Task<IEnumerable<Supplier>> GetAllAsync() =>
        await _dbContext.Suppliers.ToListAsync();

    public async Task<Supplier?> GetByIdAsync(int supplierId) =>
        await _dbContext.Suppliers.FindAsync(supplierId);

    public void Insert(Supplier supplier) => _dbContext.Suppliers.Add(supplier);

    public void Remove(Supplier supplier) => _dbContext.Suppliers.Remove(supplier);

    public Task<bool> IdExists(int id) => _dbContext.Suppliers.AnyAsync(s => s.Id == id);

    public Task<bool> PhoneExists(string phone) => _dbContext.Suppliers.AnyAsync(s => s.Phone == phone);

    public Task<bool> EmailExists(string email) => _dbContext.Suppliers.AnyAsync(s => s.Email == email);
    
    public Task<bool> CompanyNameExists(string companyName) => _dbContext.Suppliers.AnyAsync(s => s.CompanyName == companyName);
}
