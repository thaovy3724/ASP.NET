using MediatR;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Command.Remove
{
    public sealed record RemoveFavoriteCommand(
        Guid? CustomerId,
        Guid ProductId
    ) : IRequest;
}