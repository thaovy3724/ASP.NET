using MediatR;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Confirm
{
    public sealed record ConfirmOrderCommand (Guid Id) : IRequest<Result>;
}
