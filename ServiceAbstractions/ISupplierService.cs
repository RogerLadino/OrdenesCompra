using Shared.DTOs;

namespace Service.Abstractions;

public interface ISupplierService : IServiceBase<SupplierDto>
{
    Task<SupplierDto> CreateAsync(SupplierCreationDto supplierDto);
}
