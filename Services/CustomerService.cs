using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;
using Shared.DTOs.Customers;
using Domain.Exceptions;

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
        return customers.Adapt<IEnumerable<CustomerDto>>();
    }

    public async Task<CustomerDto> GetByIdAsync(int customerId)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);

        if(customer is null)
        {
            throw new CustomerNotFoundException("No customer exists with given ID");
        }

        return customer.Adapt<CustomerDto>();
    }

    public async Task<CustomerDto> CreateAsync(CustomerCreationDto customerForCreationDto)
    {
        var customer = customerForCreationDto.Adapt<Customer>();

        var emailExists = await _repositoryManager.CustomerRepository
            .AnyAsync(c => c.Email.Equals(customer.Email));

        if (emailExists)
        {
            throw new EmailAlreadyExistsException("Email provided already exists");
        }

        var phoneExists = await _repositoryManager.CustomerRepository
            .AnyAsync(c => c.Phone.Equals(customer.Phone));

        if (phoneExists)
        {
            throw new PhoneAlreadyExistsException("Phone number provided already exists");
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
            throw new CustomerNotFoundException("No customer exists with given ID");
        }

        customerForUpdateDto.Adapt(customer);

        var emailExists = await _repositoryManager.CustomerRepository
            .AnyAsync(c => c.Email.Equals(customer.Email) && c.Id != customerId);

        if (emailExists)
        {
            throw new EmailAlreadyExistsException("Email provided is already used");
        }

        var phoneExists = await _repositoryManager.CustomerRepository
            .AnyAsync(c => c.Phone.Equals(customer.Phone) && c.Id != customerId);

        if (phoneExists)
        {
            throw new PhoneAlreadyExistsException("Phone number provided is already used");
        }

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int customerId)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);

        if (customer is null)
        {
            throw new CustomerNotFoundException("No customer exists with given ID");
        }

        _repositoryManager.CustomerRepository.Remove(customer);
        await _repositoryManager.SaveChangesAsync();
    }
}
