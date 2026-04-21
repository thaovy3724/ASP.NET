using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;

namespace StoreApp.Application.UseCases.CartUseCase.Command.AddItem
{
    public class AddCartItemHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository
    ) : IRequestHandler<AddCartItemCommand, CartDTO>
    {
        public async Task<CartDTO> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetById(request.ProductId);
            if (product is null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại.");
            }

            product.EnsureCanBeOrdered();

            if (product.Quantity <= 0)
            {
                throw new ConflictException("Sản phẩm hiện đã hết hàng.");
            }

            var cart = await cartRepository.GetOrCreateByCustomerId(request.CustomerId!.Value);

            var existingItem = await cartRepository.GetCartItem(cart.Id, request.ProductId);

            var newQuantity = request.Quantity;

            if (existingItem is not null)
            {
                newQuantity += existingItem.Quantity;
            }

            if (newQuantity > product.Quantity)
            {
                throw new ConflictException($"Sản phẩm '{product.ProductName}' không đủ hàng. Còn lại: {product.Quantity}.");
            }

            if (existingItem is null)
            {
                var cartItem = new CartItem(cart.Id, request.ProductId, request.Quantity);
                cartRepository.AddCartItem(cartItem);
            }
            else
            {
                existingItem.UpdateQuantity(newQuantity);
            }

            await cartRepository.SaveChangesAsync();

            cart = await cartRepository.GetByCustomerIdWithItems(request.CustomerId.Value)
                   ?? throw new NotFoundException("Giỏ hàng không tồn tại.");

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