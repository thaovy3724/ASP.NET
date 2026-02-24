using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Results;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public sealed record CreateOrderCommand(
        Guid CustomerId,
        List<CreateOrderRequestItem> Items,
        string PaymentMethod = "Cash"  // Mặc định là tiền mặt
    ) : IRequest<ResultWithData<OrderDTO>>;
}