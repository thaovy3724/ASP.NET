using MediatR;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Deliver
{
    public sealed record DeliverOrderCommand(Guid Id) : IRequest<Unit>;
}
