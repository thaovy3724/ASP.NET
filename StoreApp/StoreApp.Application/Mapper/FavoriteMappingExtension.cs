using StoreApp.Application.DTOs;
using StoreApp.Core.Entities;

namespace StoreApp.Application.Mapper
{
    public static class FavoriteMappingExtension
    {
        public static FavoriteDTO ToDTO(this Favorite entity)
        {
            return new FavoriteDTO(
                Id: entity.Id,
                ProductId: entity.ProductId,
                ProductName: entity.Product?.ProductName ?? "",
                Price: entity.Product?.Price ?? 0,
                Quantity: entity.Product?.Quantity ?? 0,
                ImageUrl: entity.Product?.ImageUrl ?? "",
                CreatedAt: entity.CreatedAt
            );
        }
    }
}