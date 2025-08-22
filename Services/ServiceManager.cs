using Domain.Repositories;
using Service.Abstractions;
using Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICustomerService> _lazyCustomerService;
    private readonly Lazy<IProductService> _lazyProductService;
    private readonly Lazy<ISupplierService> _lazySupplierService;

    // Las clases serán creadas de manera "perezosa"
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyCustomerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager));
        _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager));
        _lazySupplierService = new Lazy<ISupplierService>(() => new SupplierService(repositoryManager));
    }

    public ICustomerService CustomerService => _lazyCustomerService.Value;
    public IProductService ProductService => _lazyProductService.Value;
    public ISupplierService SupplierService => _lazySupplierService.Value;
}
