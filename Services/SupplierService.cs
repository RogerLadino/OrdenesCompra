using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;
using Shared.DTOs.Supplier;
using Domain.Exceptions;

namespace Services;

public class SupplierService : ISupplierService
{
    private readonly IRepositoryManager _repositoryManager;

    public SupplierService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<IEnumerable<SupplierDto>> GetAllAsync()
    {
        var suppliers = await _repositoryManager.SupplierRepository.GetAllAsync();
        return suppliers.Adapt<IEnumerable<SupplierDto>>();
    }

    public async Task<SupplierDto?> GetByIdAsync(int supplierId)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);

        if (supplier is null)
            throw new SupplierNotFoundException("No supplier exists with given ID");

        return supplier?.Adapt<SupplierDto>();
    }

    public async Task<SupplierDto> CreateAsync(SupplierCreationDto supplierForCreationDto)
    {
        var supplier = supplierForCreationDto.Adapt<Supplier>();

        var companyNameExists = await _repositoryManager.SupplierRepository
            .AnyAsync(s => s.CompanyName.Equals(supplier.CompanyName) && s.Id != supplier.Id);

        if (companyNameExists)
            throw new CompanyNameAlreadyExistsException("Company name provided already exists");

        var emailExists = await _repositoryManager.SupplierRepository
            .AnyAsync(s => s.Email.Equals(supplier.Email) && s.Id != supplier.Id);

        if (emailExists)
            throw new EmailAlreadyExistsException("Email provided already exists");

        var phoneExists = await _repositoryManager.SupplierRepository
            .AnyAsync(s => s.Phone.Equals(supplier.Phone) && s.Id != supplier.Id);

        if (phoneExists)
            throw new PhoneAlreadyExistsException("Phone number provided already exists");

        _repositoryManager.SupplierRepository.Add(supplier);
        await _repositoryManager.SaveChangesAsync();

        return supplier.Adapt<SupplierDto>();
    }

    public async Task UpdateAsync(int supplierId, SupplierDto supplierForUpdateDto)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);

        if (supplier is null)
            throw new SupplierNotFoundException("No supplier exists with given ID")

        supplierForUpdateDto.Adapt(supplier);

        var companyNameExists = await _repositoryManager.SupplierRepository
            .AnyAsync(s => s.CompanyName.Equals(supplier.CompanyName) && s.Id != supplierId);

        if (companyNameExists)
            throw new CompanyNameAlreadyExistsException("Company name provided already exists");

        var emailExists = await _repositoryManager.SupplierRepository
            .AnyAsync(s => s.Email.Equals(supplier.Email) && s.Id != supplierId);

        if (emailExists)
            throw new EmailAlreadyExistsException("Email provided already exists");

        var phoneExists = await _repositoryManager.SupplierRepository
            .AnyAsync(s => s.Phone.Equals(supplier.Phone) && s.Id != supplierId);

        if (phoneExists)
            throw new PhoneAlreadyExistsException("Phone number provided already exists");

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int supplierId)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);
        if (supplier is null)
            throw new SupplierNotFoundException("No supplier exists with given ID");

        _repositoryManager.SupplierRepository.Remove(supplier);
        await _repositoryManager.SaveChangesAsync();
    }
}
