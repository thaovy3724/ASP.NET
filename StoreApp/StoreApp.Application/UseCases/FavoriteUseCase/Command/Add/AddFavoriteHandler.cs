using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Command.Add
{
    public class AddFavoriteHandler(
        IFavoriteRepository favoriteRepository,
        IProductRepository productRepository)
        : IRequestHandler<AddFavoriteCommand, FavoriteDTO>
    {
        public async Task<FavoriteDTO> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;

            var product = await productRepository.GetById(request.ProductId);

            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            var existed = await favoriteRepository.HasFavorite(customerId, request.ProductId);

            if (existed)
            {
                throw new ConflictException("Sản phẩm đã có trong danh sách yêu thích.");
            }

            var favorite = new Favorite(customerId, request.ProductId);

            await favoriteRepository.Create(favorite);

            return new FavoriteDTO(
                Id: favorite.Id,
                ProductId: product.Id,
                ProductName: product.ProductName,
                Price: product.Price,
                Quantity: product.Quantity,
                ImageUrl: product.ImageUrl,
                CreatedAt: favorite.CreatedAt
            );
        }
    }
}