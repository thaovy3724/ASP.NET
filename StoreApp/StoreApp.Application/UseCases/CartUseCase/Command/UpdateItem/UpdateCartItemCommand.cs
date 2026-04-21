using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CartUseCase.Command.UpdateItem
{
    public sealed record UpdateCartItemRequest(
        int Quantity
    );

    public sealed record UpdateCartItemCommand(
        Guid ProductId,
        int Quantity,
        Guid? CustomerId
    ) : IRequest<CartDTO>;
}