using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;

namespace StoreApp.Application.UseCases.CartUseCase.Command.UpdateItem
{
    public class UpdateCartItemHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository
    ) : IRequestHandler<UpdateCartItemCommand, CartDTO>
    {
        public async Task<CartDTO> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
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

            var product = await productRepository.GetById(request.ProductId);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            product.EnsureCanBeOrdered();

            if (request.Quantity > product.Quantity)
            {
                throw new ConflictException($"Sản phẩm '{product.ProductName}' không đủ hàng. Còn lại: {product.Quantity}.");
            }

            item.UpdateQuantity(request.Quantity);

            await cartRepository.SaveChangesAsync();

            var productIds = cart.Items
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            var products = await productRepository.GetByIds(productIds);

            return cart.ToDTO(products);
        }
    }
}