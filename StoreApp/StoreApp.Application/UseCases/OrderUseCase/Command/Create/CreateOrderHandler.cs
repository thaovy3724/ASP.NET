using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public class CreateOrderHandler { }
    //public class CreateOrderHandler(
    //    IOrderRepository orderRepository,
    //    IProductRepository productRepository,
    //    IVnPayService vnPayService // INJECT THÊM SERVICE VNPAY
    //) : IRequestHandler<CreateOrderCommand, ResultWithData<OrderDTO>>
    //{
    //    public async Task<ResultWithData<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    //    {
    //        // --- 1. VALIDATION LAYER ---
    //        if (request.Items == null || !request.Items.Any())
    //            return new ResultWithData<OrderDTO>(false, "Đơn hàng phải có ít nhất 1 sản phẩm.", null);

    //        if (request.Items.Any(x => x.Quantity <= 0))
    //            return new ResultWithData<OrderDTO>(false, "Số lượng sản phẩm phải lớn hơn 0.", null);

    //        var customer = await customerRepository.GetById((Guid)request.CustomerId);
    //        if (customer == null)
    //            return new ResultWithData<OrderDTO>(false, "Khách hàng không tồn tại.", null);

    //        // --- 2. BUSINESS LOGIC LAYER (Transaction) ---
    //        await orderRepository.BeginTransactionAsync();

    //        try
    //        {
    //            // A. Khởi tạo Order
    //            var order = new Order(
    //                customerId: request.CustomerId,
    //                userId: request.UserId,
    //                promoId: request.PromoId,
    //                orderDate: DateTime.UtcNow.AddHours(7),
    //                orderStatus: OrderStatus.Pending,
    //                discountAmount: 0
    //            );

    //            // B. Xử lý từng sản phẩm (Giữ nguyên logic cũ)
    //            foreach (var item in request.Items)
    //            {
    //                var product = await productRepository.GetById(item.ProductId);
    //                if (product == null)
    //                {
    //                    await orderRepository.RollbackTransactionAsync();
    //                    return new ResultWithData<OrderDTO>(false, $"Sản phẩm ID {item.ProductId} không tồn tại.", null);
    //                }

    //                var inventory = await inventoryRepository.GetByProductID(item.ProductId);
    //                if (inventory == null || inventory.Quantity < item.Quantity)
    //                {
    //                    await orderRepository.RollbackTransactionAsync();
    //                    return new ResultWithData<OrderDTO>(false, $"Sản phẩm '{product.ProductName}' không đủ hàng (Còn: {inventory?.Quantity ?? 0}).", null);
    //                }

    //                await inventoryRepository.deductQuantityOfCreatedOrder(item.ProductId, item.Quantity);

    //                order.Items.Add(new OrderDetail(
    //                    orderId: order.Id,
    //                    productId: item.ProductId,
    //                    quantity: item.Quantity,
    //                    price: product.Price
    //                ));
    //            }

    //            // C. Tính toán giảm giá (Giữ nguyên logic cũ)
    //            if (request.PromoId.HasValue)
    //            {
    //                var promotion = await promotionRepository.GetById(request.PromoId.Value);
    //                if (promotion != null && promotion.IsActive())
    //                {
    //                    decimal discount = 0;
    //                    decimal currentSubTotal = order.Items.Sum(x => x.Subtotal);

    //                    if (promotion.DiscountType == DiscountType.Percentage)
    //                        discount = currentSubTotal * (promotion.DiscountValue / 100);
    //                    else if (promotion.DiscountType == DiscountType.FixedAmount)
    //                        discount = promotion.DiscountValue;

    //                    discount = Math.Min(discount, currentSubTotal);
    //                    order.SetDiscount(discount);
    //                }
    //            }

    //            // D. Tính tổng tiền và Lưu Order
    //            order.CalculateTotal();
    //            await orderRepository.Create(order); // LÚC NÀY ĐÃ CÓ ORDER ID

    //            // E. Tạo Payment

    //            if (request.PaymentMethod != "VnPay")
    //            {
    //                // 1. Xác định phương thức (Mặc định là Cash)
    //                var methodEnum = PaymentMethod.Cash;

    //                var payment = new Payment(
    //                    order.Id,
    //                    order.TotalAmount,
    //                    methodEnum
    //                );

    //                await paymentRepository.Create(payment);
    //            }

    //            // F. CHỐT TRANSACTION
    //            await orderRepository.CommitTransactionAsync();

    //            // --- 3. POST-PROCESSING (Xử lý sau khi lưu thành công) ---


    //            // 1. Map dữ liệu thô từ Entity ra DTO
    //            // Lúc này PaymentMethod đang là "" và PaymentUrl là null (do hàm ToDTO ở trên)
    //            var orderResponse = order.ToDTO();

    //            // 2. Cập nhật PaymentMethod chuẩn từ Request
    //            // Dùng 'with' để thay thế giá trị string.Empty ban đầu
    //            orderResponse = orderResponse with { PaymentMethod = request.PaymentMethod };

    //            // 3. Logic VNPay tạo URL
    //            if (request.PaymentMethod == "VnPay")
    //            {
    //                // Tạo URL
    //                string url = vnPayService.CreatePaymentUrl(orderResponse);

    //                // Cập nhật tiếp PaymentUrl
    //                orderResponse = orderResponse with { PaymentUrl = url };
    //            }

    //            return new ResultWithData<OrderDTO>(
    //                true,
    //                "Tạo đơn hàng thành công",
    //                orderResponse
    //            );
    //        }
    //        catch (Exception ex)
    //        {
    //            await orderRepository.RollbackTransactionAsync();
    //            return new ResultWithData<OrderDTO>(false, "Lỗi hệ thống: " + ex.Message, null);
    //        }
    //    }
    //}
}