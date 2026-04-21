using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderRequest(
        string Address,
        string PaymentMethod
    );

    public sealed record CreateOrderCommand(
        string Address,
        string PaymentMethod,
        Guid? CustomerId
    ) : IRequest<CreateOrderResponseDTO>;
}