using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;

namespace Services;

public class CustomerService : ICustomerService
{
    private readonly IRepositoryManager _repositoryManager;

    public CustomerService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _repositoryManager.CustomerRepository.GetAllAsync();

        var customerDtos = customers.Adapt<IEnumerable<CustomerDto>>();

        return customerDtos;
    }

    public async Task<CustomerDto?> GetByIdAsync(int customerId)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            return null;
        }

        var customerDto = customer.Adapt<CustomerDto>();

        return customerDto;
    }

    public async Task<CustomerDto> CreateAsync(CustomerDto customerForCreationDto)
    {
        var customer = customerForCreationDto.Adapt<Customer>();

        var idExists = await _repositoryManager.CustomerRepository.AnyAsync(c => c.Id.Equals(customer.Id));

        if (idExists)
        {
            return null;
        }

        var emailExists = await _repositoryManager.CustomerRepository.AnyAsync(c => c.Email.Equals(customer.Email));

        if (emailExists)
        {
            return null;
        }

        var phoneExists = await _repositoryManager.CustomerRepository.AnyAsync(c => c.Phone.Equals(customer.Phone));

        if (phoneExists)
        {
            return null;
        }

        _repositoryManager.CustomerRepository.Add(customer);

        await _repositoryManager.SaveChangesAsync();

        return customer.Adapt<CustomerDto>();
    }

    public async Task UpdateAsync(int customerId, CustomerDto customerForUpdateDto)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            return;
        }

        customerForUpdateDto.Adapt(customer);

        var emailExists = await _repositoryManager.CustomerRepository.AnyAsync(c => c.Email.Equals(customer.Email));

        if (emailExists)
        {
            return;
        }

        var phoneExists = await _repositoryManager.CustomerRepository.AnyAsync(c => c.Phone.Equals(customer.Phone));

        if (phoneExists)
        {
            return;
        }

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int customerId)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            return;
        }

        _repositoryManager.CustomerRepository.Remove(customer);

        await _repositoryManager.SaveChangesAsync();
    }
}