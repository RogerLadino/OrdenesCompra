using Domain.Repositories;
using Persistence.Repositories;

namespace Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryDbContext _dbContext;

        private readonly Lazy<ICustomerRepository> _lazyCustomerRepository;
        private readonly Lazy<IProductRepository> _lazyProductRepository;
        private readonly Lazy<ISupplierRepository> _lazySupplierRepository;
        private readonly Lazy<IOrderRepository> _lazyOrdersRepository;  

        public RepositoryManager(RepositoryDbContext dbContext)
        {
            _dbContext = dbContext;

            _lazyCustomerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_dbContext));
            _lazyProductRepository = new Lazy<IProductRepository>(() => new ProductRepository(_dbContext));
            _lazySupplierRepository = new Lazy<ISupplierRepository>(() => new SupplierRepository(_dbContext));
            _lazyOrdersRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_dbContext));
        }

        public ICustomerRepository CustomerRepository => _lazyCustomerRepository.Value;
        public IProductRepository ProductRepository => _lazyProductRepository.Value;
        public ISupplierRepository SupplierRepository => _lazySupplierRepository.Value;
        public IOrderRepository OrderRepository => _lazyOrdersRepository.Value;
        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}