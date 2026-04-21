namespace StoreApp.Application.DTOs
{
    public sealed record CustomerAddressDTO(
        Guid Id,
        Guid CustomerId,
        string ReceiverName,
        string Phone,
        string AddressLine,
        bool IsDefault,
        DateTime CreatedAt
    );
}
