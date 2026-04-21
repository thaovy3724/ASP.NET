using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Query.GetList
{
    public sealed record GetListFavoriteQuery(
        Guid? CustomerId,
        int PageNumber = 1,
        int PageSize = 8
    ) : IRequest<PagedList<FavoriteDTO>>;
}