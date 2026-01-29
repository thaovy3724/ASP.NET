using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.SupplierUseCase.Command.Remove
{
    public sealed record RemoveSupplierCommand(Guid Id) : IRequest<Result> ;
}
