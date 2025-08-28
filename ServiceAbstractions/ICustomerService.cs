using Shared.DTOs.Customers;

namespace Service.Abstractions;

public interface ICustomerService : IServiceBase<CustomerDto>
{
    Task<CustomerDto> CreateAsync(CustomerCreationDto customer);
}