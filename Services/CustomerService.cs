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
        if (await _repositoryManager.CustomerRepository.IdExists(customer.Id))
        {
            return null;
        }
        if(await _repositoryManager.CustomerRepository.EmailExists(customer.Email))
        {
            return null;
        }
        if(await _repositoryManager.CustomerRepository.PhoneExists(customer.Phone))
        {
            return null;
        }
        _repositoryManager.CustomerRepository.Insert(customer);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return customer.Adapt<CustomerDto>();
    }

    public async Task<CustomerDto?> UpdateAsync(int customerId, CustomerUpdateDto customerForUpdateDto)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);
        if (customer is null)
        {
            return null;
        }
        customer.FirstName = customerForUpdateDto.FirstName;
        customer.LastName = customerForUpdateDto.LastName;
        customer.City = customerForUpdateDto.City;
        customer.Country = customerForUpdateDto.Country;
        customer.Phone = customerForUpdateDto.Phone;
        customer.Email = customerForUpdateDto.Email;
        customer.BirthDate = customerForUpdateDto.BirthDate;
        // No modificar Age porque es columna calculada
        var allCustomers = await _repositoryManager.CustomerRepository.GetAllAsync();
        if (allCustomers.Any(c => c.Email == customer.Email && c.Id != customerId))
            return null;
        if (allCustomers.Any(c => c.Phone == customer.Phone && c.Id != customerId))
            return null;
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return customer.Adapt<CustomerDto>();
    }

    public async Task DeleteAsync(int customerId)
    {
        var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId);
        if (customer is null)
        {
            return;
        }
        _repositoryManager.CustomerRepository.Remove(customer);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }
}