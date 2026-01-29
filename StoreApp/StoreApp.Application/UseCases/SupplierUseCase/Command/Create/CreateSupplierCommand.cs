using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Create
{
    public sealed record CreateSupplierCommand(
        string Name,
        string Phone,
        string Email,
        string Address
    ) : IRequest<ResultWithData<SupplierDTO>>;
}
