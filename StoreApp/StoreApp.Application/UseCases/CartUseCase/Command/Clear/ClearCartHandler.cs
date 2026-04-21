using MediatR;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CartUseCase.Command.Clear
{
    public class ClearCartHandler(
        ICartRepository cartRepository
    ) : IRequestHandler<ClearCartCommand>
    {
        public async Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByCustomerIdWithItems(request.CustomerId!.Value);

            if (cart is null || cart.Items.Count == 0)
            {
                return;
            }

            cartRepository.RemoveCartItems(cart.Items);
            await cartRepository.SaveChangesAsync();
        }
    }
}