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
        IVnPayService vnPayService // INJECT THÊM SERVICE VNPAY
    ) : IRequestHandler<CreateOrderCommand, CreateOrderResponseDTO>
    {
        public async Task<CreateOrderResponseDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (!await userRepository.IsExist(user => user.Id == request.CustomerId && user.Role == Role.Customer))
            {
                throw new NotFoundException("Khách hàng không tồn tại.");
            }

            // --- 2. BUSINESS LOGIC LAYER (Transaction) ---
            await orderRepository.BeginTransactionAsync();

            try
            {
                var pttt = Enum.Parse<PaymentMethod>(request.PaymentMethod);
                // A. Khởi tạo Order
                var order = new Order(request.CustomerId.Value, request.Address, pttt);

                // B. Xử lý từng sản phẩm (Giữ nguyên logic cũ)
                foreach (var item in request.Items)
                {
                    var product = await productRepository.GetById(item.ProductId);
                    if (product == null)
                    {
                        throw new NotFoundException($"Sản phẩm ID {item.ProductId} không tồn tại.");
                    }
                    // Kiểm tra xem sản phẩm có thể được đặt hàng không 
                    product.EnsureCanBeOrdered();

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

                // 3. Logic VNPay tạo URL
                if (pttt == PaymentMethod.VnPay)
                {
                    // Tạo URL
                    string url = vnPayService.CreatePaymentUrl(order.Id, order.TotalAmount);

                    // Cập nhật tiếp PaymentUrl
                    orderResponse = orderResponse with { PaymentUrl = url };
                }

                return orderResponse;
            }
            catch (Exception ex)
            {
                await orderRepository.RollbackTransactionAsync();
                if (ex is NotFoundException) throw;

                // Lỗi hệ thống chưa biết
                throw new Exception("Hệ thống không thể xử lý phiếu nhập kho lúc này. Vui lòng liên hệ quản trị viên.");
            }

        }
    }
}