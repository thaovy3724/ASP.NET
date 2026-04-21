using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.FavoriteUseCase.Command.Remove
{
    public class RemoveFavoriteHandler(IFavoriteRepository favoriteRepository)
        : IRequestHandler<RemoveFavoriteCommand>
    {
        public async Task Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;

            var favorite = await favoriteRepository.GetByCustomerAndProduct(customerId, request.ProductId);

            if (favorite is null)
            {
                throw new NotFoundException("Sản phẩm không có trong danh sách yêu thích.");
            }

            await favoriteRepository.Delete(favorite);
        }
    }
}