using Domain.Repositories;

namespace Persistence.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly RepositoryDbContext _dbContext;

    public UnitOfWork(RepositoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}