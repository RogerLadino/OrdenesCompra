using Domain.Entities;
using Domain.Repositories;
using Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository 
{
    public OrderRepository(RepositoryDbContext repositoryContext) : base(repositoryContext)
    {
    }

    public new async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbSet
            .Include(o => o.OrderItems)
            .ToListAsync();
    }
    public new async Task<Order?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
