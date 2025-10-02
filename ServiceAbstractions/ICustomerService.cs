using Domain.DTOs;

namespace Service.Abstractions;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();

    Task<CustomerDto?> GetByIdAsync(int customerId);

    Task<CustomerDto> CreateAsync(CustomerDto customerForCreationDto);

    Task<CustomerDto?> UpdateAsync(int customerId, CustomerUpdateDto customerForUpdateDto);

    Task DeleteAsync(int customerId);
}