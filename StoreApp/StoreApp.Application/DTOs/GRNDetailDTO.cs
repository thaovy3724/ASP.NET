using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.DTOs
{
    public sealed record GRNDetailDTO(Guid ProductId, int Quantity, decimal Price);
}
