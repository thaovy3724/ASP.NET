using MediatR;
using StoreApp.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Update
{
    public sealed record UpdateOrderStatusCommand(
        Guid OrderId,
        bool IsSuccess,
        string? TransactionId = null
    ) : IRequest<ResultWithData<string>>;
}
