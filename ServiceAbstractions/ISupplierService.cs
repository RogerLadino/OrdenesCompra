using Shared.DTOs.Supplier;

namespace Service.Abstractions;

public interface ISupplierService : IServiceBase<SupplierDto>
{
    Task<SupplierDto> CreateAsync(SupplierCreationDto supplierDto);
}
