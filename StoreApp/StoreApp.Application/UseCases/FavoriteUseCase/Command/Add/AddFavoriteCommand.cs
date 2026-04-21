using MediatR;
using StoreApp.Application.DTOs;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Command.Add
{
    public sealed record AddFavoriteCommand(
        Guid? CustomerId,
        Guid ProductId
    ) : IRequest<FavoriteDTO>;
}