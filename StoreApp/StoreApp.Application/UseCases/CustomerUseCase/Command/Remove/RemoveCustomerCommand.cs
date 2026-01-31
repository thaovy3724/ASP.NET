using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.CustomerUseCase.Command.Remove
{
    public sealed record RemoveCustomerCommand(Guid Id) : IRequest<Result> ;
}
