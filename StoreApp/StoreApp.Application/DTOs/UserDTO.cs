using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record UserDTO(
        Guid Id,
        string Username,
        string FullName,
        string Phone,
        Role Role,
        bool IsLocked
    );
}
