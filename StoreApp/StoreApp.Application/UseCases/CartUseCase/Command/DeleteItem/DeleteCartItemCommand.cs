using MediatR;

namespace StoreApp.Application.UseCases.CartUseCase.Command.DeleteItem
{
    public sealed record DeleteCartItemCommand(
        Guid ProductId,
        Guid? CustomerId
    ) : IRequest;
}