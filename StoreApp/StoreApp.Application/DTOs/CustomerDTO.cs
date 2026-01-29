using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public sealed record CustomerDTO(
        Guid Id,
        string Name,
        string Email,
        string Phone,
        string Address,
        DateTime CreatedAt
    );
}
