using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderCommand(
        List<CreateOrderItem> Items,
        string Address,
        string PaymentMethod,
        Guid? CustomerId,
        Guid? VoucherCode
        ) : IRequest<CreateOrderResponseDTO>;
}