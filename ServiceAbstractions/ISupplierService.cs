using Domain.DTOs;

namespace Service.Abstractions;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDto>> GetAllAsync();

    Task<SupplierDto?> GetByIdAsync(int supplierId);

    Task<SupplierDto> CreateAsync(SupplierDto supplierForCreationDto);

    Task UpdateAsync(int supplierId, SupplierDto supplierForUpdateDto);

    Task DeleteAsync(int supplierId);
}
