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
        ICustomerAddressRepository addressRepository,
        IVnPayService vnPayService
    ) : IRequestHandler<CreateOrderCommand, CreateOrderResponseDTO>
    {
        public async Task<CreateOrderResponseDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var customerId = request.CustomerId!.Value;

            if (!await userRepository.IsExist(user => user.Id == customerId && user.Role == Role.Customer))
            {
                throw new NotFoundException("Khách hàng không tồn tại.");
            }

            Guid? addressId = null;
            string addressSnapshot;

            if (request.AddressId.HasValue)
            {
                var address = await addressRepository.GetByIdAndCustomerIdAsync(request.AddressId.Value, customerId);
                if (address == null)
                {
                    throw new NotFoundException("Địa chỉ giao hàng không tồn tại hoặc không thuộc tài khoản của bạn.");
                }

                addressId = address.Id;
                addressSnapshot = address.ToSnapshot();
            }
            else
            {
                addressSnapshot = request.Address?.Trim() ?? string.Empty;
            }

            await orderRepository.BeginTransactionAsync();

            try
            {
                var paymentMethod = Enum.Parse<PaymentMethod>(request.PaymentMethod, true);
                var order = new Order(customerId, addressId, addressSnapshot, paymentMethod);

                foreach (var item in request.Items)
                {
                    var product = await productRepository.GetById(item.ProductId);
                    if (product == null)
                    {
                        throw new NotFoundException($"Sản phẩm ID {item.ProductId} không tồn tại.");
                    }

                    var isSuccess = await productRepository.DecreaseStockIfAvailable(item.ProductId, item.Quantity);
                    if (!isSuccess)
                    {
                        throw new ConflictException($"Sản phẩm '{product.ProductName}' không đủ hàng (Còn: {product.Quantity}).");
                    }

                    order.AddItem(item.ProductId, item.Quantity, item.Price);
                }

                await orderRepository.Create(order);
                await orderRepository.CommitTransactionAsync();

                var orderResponse = order.ToCreateOrderResponseDTO();

                if (paymentMethod == PaymentMethod.VnPay)
                {
                    string url = vnPayService.CreatePaymentUrl(order.Id, order.TotalAmount);
                    orderResponse = orderResponse with { PaymentUrl = url };
                }

                return orderResponse;
            }
            catch (Exception ex)
            {
                await orderRepository.RollbackTransactionAsync();

                if (ex is NotFoundException or ConflictException or BadRequestException)
                    throw;

                throw new Exception("Hệ thống không thể xử lý đơn hàng lúc này. Vui lòng liên hệ quản trị viên.");
            }
        }
    }
}
