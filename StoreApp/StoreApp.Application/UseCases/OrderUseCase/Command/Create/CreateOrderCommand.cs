using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderCommand(
        Guid CustomerId,
        List<CreateOrderItem> Items,
        string Address,
        PaymentMethod PaymentMethod) : IRequest<CreateOrderResponseDTO>;
}