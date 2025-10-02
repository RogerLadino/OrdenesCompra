using Domain.DTOs;

namespace Service.Abstractions;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllAsync();

    Task<SupplierDto?> GetByIdAsync(int supplierId);

    Task<SupplierDto> CreateAsync(SupplierDto supplierForCreationDto);

    Task<SupplierDto?> UpdateAsync(int supplierId, SupplierUpdateDto supplierForUpdateDto);

    Task DeleteAsync(int supplierId);
}
