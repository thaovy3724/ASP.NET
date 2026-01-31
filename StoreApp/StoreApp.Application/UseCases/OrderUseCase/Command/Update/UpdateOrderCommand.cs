using MediatR;
using StoreApp.Application.Results;
using StoreApp.Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Update
{
    public sealed record UpdateOrderCommand(Guid Id, OrderStatus Status) : IRequest<Result>;
}
