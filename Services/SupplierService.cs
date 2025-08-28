using Domain.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Mapster;

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
        if (supplier is null) return null;
        return supplier.Adapt<SupplierDto>();
    }

    public async Task<SupplierDto?> CreateAsync(SupplierDto supplierForCreationDto)
    {
        var supplier = supplierForCreationDto.Adapt<Supplier>();

        var idExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.Id.Equals(supplier.Id));

        if (idExists)
            return null;

        var companyNameExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.CompanyName.Equals(supplier.CompanyName));

        if (companyNameExists)
            return null;

        var emailExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.Email.Equals(supplier.Email));

        if (emailExists)
            return null;

        var phoneExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.Phone.Equals(supplier.Phone));

        if (phoneExists)
            return null;

        _repositoryManager.SupplierRepository.Add(supplier);
        
        await _repositoryManager.SaveChangesAsync();

        return supplier.Adapt<SupplierDto>();
    }

    public async Task UpdateAsync(int supplierId, SupplierDto supplierForUpdateDto)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);
        if (supplier is null) return;

        supplierForUpdateDto.Adapt(supplier);

        var companyNameExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.CompanyName.Equals(supplier.CompanyName));

        if (companyNameExists)
            return;

        var emailExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.Email.Equals(supplier.Email));

        if (emailExists)
            return;

        var phoneExists = await _repositoryManager.SupplierRepository.AnyAsync(s => s.Phone.Equals(supplier.Phone));

        if (phoneExists)
            return;

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int supplierId)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);
        if (supplier is null) return;

        _repositoryManager.SupplierRepository.Remove(supplier);
        await _repositoryManager.SaveChangesAsync();
    }
}
