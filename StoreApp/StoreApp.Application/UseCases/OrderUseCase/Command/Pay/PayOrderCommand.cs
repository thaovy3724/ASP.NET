using MediatR;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Pay
{
    public sealed record PayOrderCommand(Guid Id) : IRequest<Unit>;
}
