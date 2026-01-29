using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
