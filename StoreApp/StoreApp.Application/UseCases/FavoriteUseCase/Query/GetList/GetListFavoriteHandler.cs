using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Query.GetList
{
    public class GetListFavoriteHandler(IFavoriteRepository favoriteRepository)
        : IRequestHandler<GetListFavoriteQuery, PagedList<FavoriteDTO>>
    {
        public async Task<PagedList<FavoriteDTO>> Handle(GetListFavoriteQuery request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;

            var favorites = await favoriteRepository.Search(
                customerId,
                request.PageNumber,
                request.PageSize
            );

            var dtos = favorites.Items
                .Select(x => x.ToDTO())
                .ToList();

            return new PagedList<FavoriteDTO>(dtos, favorites.MetaData);
        }
    }
}