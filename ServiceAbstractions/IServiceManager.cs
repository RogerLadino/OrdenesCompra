namespace Service.Abstractions;
public interface IServiceManager
{
    ICustomerService CustomerService { get; }
    IProductService ProductService { get; }
    ISupplierService SupplierService { get; }

    IOrderService OrderService {  get; }
}
