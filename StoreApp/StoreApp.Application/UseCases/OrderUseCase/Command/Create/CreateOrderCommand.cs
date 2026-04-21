using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderCommand(
        List<CreateOrderItem> Items,
        Guid? AddressId,
        string? Address,
        string PaymentMethod,
        Guid? CustomerId) : IRequest<CreateOrderResponseDTO>;
}
