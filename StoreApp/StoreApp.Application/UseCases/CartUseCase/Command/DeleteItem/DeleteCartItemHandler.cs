using MediatR;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CartUseCase.Command.DeleteItem
{
    public class DeleteCartItemHandler(
        ICartRepository cartRepository
    ) : IRequestHandler<DeleteCartItemCommand>
    {
        public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByCustomerIdWithItems(request.CustomerId!.Value);
            if (cart is null)
            {
                throw new NotFoundException("Giỏ hàng không tồn tại.");
            }

            var item = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (item is null)
            {
                throw new NotFoundException("Sản phẩm không có trong giỏ hàng.");
            }

            cartRepository.RemoveCartItem(item);
            await cartRepository.SaveChangesAsync();
        }
    }
}