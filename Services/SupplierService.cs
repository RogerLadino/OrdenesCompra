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

        if (await _repositoryManager.SupplierRepository.IdExists(supplier.Id))
            return null;

        if (await _repositoryManager.SupplierRepository.CompanyNameExists(supplier.CompanyName))
            return null;

        if (await _repositoryManager.SupplierRepository.EmailExists(supplier.Email))
            return null;

        if (await _repositoryManager.SupplierRepository.PhoneExists(supplier.Phone))
            return null;

        _repositoryManager.SupplierRepository.Insert(supplier);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();

        return supplier.Adapt<SupplierDto>();
    }

    public async Task UpdateAsync(int supplierId, SupplierDto supplierForUpdateDto)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);
        if (supplier is null) return;

        supplierForUpdateDto.Adapt(supplier);

        if (await _repositoryManager.SupplierRepository.IdExists(supplier.Id))
            return;

        if (await _repositoryManager.SupplierRepository.CompanyNameExists(supplier.CompanyName))
            return;

        if (await _repositoryManager.SupplierRepository.EmailExists(supplier.Email))
            return;

        if (await _repositoryManager.SupplierRepository.PhoneExists(supplier.Phone))
            return;

        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }

    public async Task<SupplierDto?> UpdateAsync(int supplierId, SupplierUpdateDto supplierForUpdateDto)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);
        if (supplier is null)
        {
            return null;
        }

        supplier.CompanyName = supplierForUpdateDto.CompanyName;
        supplier.ContactName = supplierForUpdateDto.ContactName;
        supplier.ContactTitle = supplierForUpdateDto.ContactTitle;
        supplier.City = supplierForUpdateDto.City;
        supplier.Country = supplierForUpdateDto.Country;
        supplier.Phone = supplierForUpdateDto.Phone;
        supplier.Email = supplierForUpdateDto.Email;
        supplier.Fax = supplierForUpdateDto.Fax;

        // Validar que no exista otro supplier con el mismo nombre
        var allSuppliers = await _repositoryManager.SupplierRepository.GetAllAsync();
        if (allSuppliers.Any(s => s.CompanyName == supplier.CompanyName && s.Id != supplierId))
            return null;

        await _repositoryManager.UnitOfWork.SaveChangesAsync();
        return supplier.Adapt<SupplierDto>();
    }

    public async Task DeleteAsync(int supplierId)
    {
        var supplier = await _repositoryManager.SupplierRepository.GetByIdAsync(supplierId);
        if (supplier is null) return;

        _repositoryManager.SupplierRepository.Remove(supplier);
        await _repositoryManager.UnitOfWork.SaveChangesAsync();
    }
}
