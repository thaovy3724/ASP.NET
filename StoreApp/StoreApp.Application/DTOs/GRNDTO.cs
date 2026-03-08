using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record GRNDTO(
        Guid Id,
        Guid SupplierId,
        GRNStatus GRNStatus,
        DateTime UpdatedAt,
        List<GRNDetailDTO> Items
    );
}