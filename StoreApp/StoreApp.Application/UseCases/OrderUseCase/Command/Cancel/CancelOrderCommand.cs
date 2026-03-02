using MediatR;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Cancel
{
    public sealed record CancelOrderCommand(Guid Id) : IRequest<Unit>;
}
