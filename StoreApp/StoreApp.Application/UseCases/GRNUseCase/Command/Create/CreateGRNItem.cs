using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.GRNUseCase.Command.Create
{
    public sealed record CreateGRNItem(Guid ProductId, int Quantity, decimal Price);
}
