using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Cancel
{
    public sealed record CancelOrderCommand(Guid Id) : IRequest<Result>;
}
