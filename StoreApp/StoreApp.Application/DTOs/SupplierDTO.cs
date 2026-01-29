using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public sealed record SupplierDTO(Guid Id, string Name, string Phone, string Email, string Address);
}
