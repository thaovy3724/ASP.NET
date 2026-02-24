using StoreApp.Core.ValueObject;

namespace StoreApp.Application.DTOs
{
    public sealed record UserDTO(
        Guid Id,
        string Username,
        string Password,
        string FullName,
        Role Role, 
        DateTime CreatedAt
    );
}
