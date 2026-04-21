using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.CartUseCase.Command.AddItem
{
    public sealed record AddCartItemRequest(
        Guid ProductId,
        int Quantity
    );

    public sealed record AddCartItemCommand(
        Guid ProductId,
        int Quantity,
        Guid? CustomerId
    ) : IRequest<CartDTO>;
}