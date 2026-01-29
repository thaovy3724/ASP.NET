using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Update
{
    public sealed record UpdateSupplierCommand(Guid Id,
        string Name,
        string Phone,
        string Email,
        string Address
    ) : IRequest<Result>;
}
