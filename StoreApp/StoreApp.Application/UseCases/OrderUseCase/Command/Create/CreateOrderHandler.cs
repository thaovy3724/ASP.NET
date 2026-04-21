using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Exceptions;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Service.Payment;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public class CreateOrderHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        ICartRepository cartRepository,
        IVnPayService vnPayService
    ) : IRequestHandler<CreateOrderCommand, CreateOrderResponseDTO>
    {
        public async Task<CreateOrderResponseDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;

            var isCustomerExist = await userRepository.IsExist(user =>
                user.Id == customerId &&
                user.Role == Role.Customer &&
                !user.IsLocked
            );

            if (!isCustomerExist)
            {
                throw new NotFoundException("Khách hàng không tồn tại hoặc đã bị khóa.");
            }

            var cart = await cartRepository.GetByCustomerIdWithItems(customerId);

            if (cart is null || cart.Items.Count == 0)
            {
                throw new BadRequestException("Giỏ hàng đang trống, không thể tạo đơn hàng.");
            }

            await orderRepository.BeginTransactionAsync();

            try
            {
                var paymentMethod = Enum.Parse<PaymentMethod>(request.PaymentMethod, true);

                var order = new Order(customerId, request.Address, paymentMethod);

                var productIds = cart.Items
                    .Select(x => x.ProductId)
                    .Distinct()
                    .ToList();

                var products = await productRepository.GetByIds(productIds);
                var productMap = products.ToDictionary(x => x.Id, x => x);

                foreach (var cartItem in cart.Items)
                {
                    if (!productMap.TryGetValue(cartItem.ProductId, out var product))
                    {
                        throw new NotFoundException($"Sản phẩm ID {cartItem.ProductId} không tồn tại.");
                    }

                    product.EnsureCanBeOrdered();

                    if (cartItem.Quantity > product.Quantity)
                    {
                        throw new ConflictException($"Sản phẩm '{product.ProductName}' không đủ hàng. Còn lại: {product.Quantity}.");
                    }

                    var decreaseSuccess = await productRepository.DecreaseStockIfAvailable(product.Id, cartItem.Quantity);

                    if (!decreaseSuccess)
                    {
                        throw new ConflictException($"Sản phẩm '{product.ProductName}' không đủ hàng.");
                    }

                    order.AddItem(product.Id, cartItem.Quantity, product.Price);
                }

                await orderRepository.Create(order);

                cartRepository.RemoveCartItems(cart.Items);
                await cartRepository.SaveChangesAsync();

                await orderRepository.CommitTransactionAsync();

                var response = order.ToCreateOrderResponseDTO();

                if (paymentMethod == PaymentMethod.VnPay)
                {
                    var paymentUrl = vnPayService.CreatePaymentUrl(order.Id, order.TotalAmount);
                    response = response with { PaymentUrl = paymentUrl };
                }

                return response;
            }
            catch
            {
                await orderRepository.RollbackTransactionAsync();
                throw;
            }
        }
    }
}