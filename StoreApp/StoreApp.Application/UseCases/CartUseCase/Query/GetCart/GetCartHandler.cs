using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CartUseCase.Query.GetCart
{
    public class GetCartHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository
    ) : IRequestHandler<GetCartQuery, CartDTO>
    {
        public async Task<CartDTO> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetOrCreateByCustomerId(request.CustomerId!.Value);

            var productIds = cart.Items
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            var products = productIds.Count == 0
                ? []
                : await productRepository.GetByIds(productIds);

            return cart.ToDTO(products);
        }
    }
}