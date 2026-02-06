using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public class CreateOrderHandler(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository,
        IPromotionRepository promotionRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IInventoryRepository inventoryRepository
    ) : IRequestHandler<CreateOrderCommand, ResultWithData<OrderDTO>>
    {
        public async Task<ResultWithData<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // --- 1. VALIDATION LAYER (Kiểm tra dữ liệu đầu vào) ---

            // Check giỏ hàng rỗng
            if (request.Items == null || !request.Items.Any())
            {
                return new ResultWithData<OrderDTO>(false, "Đơn hàng phải có ít nhất 1 sản phẩm.", null);
            }

            // Check số lượng không hợp lệ (nhỏ hơn hoặc bằng 0)
            if (request.Items.Any(x => x.Quantity <= 0))
            {
                return new ResultWithData<OrderDTO>(false, "Số lượng sản phẩm phải lớn hơn 0.", null);
            }

            // Check Khách hàng
            var customer = await customerRepository.GetById((Guid) request.CustomerId);
            if (customer == null)
            {
                return new ResultWithData<OrderDTO>(false, "Khách hàng không tồn tại.", null);
            }

            // --- 2. BUSINESS LOGIC LAYER (Transaction) ---

            // Bắt đầu Transaction từ Repository (theo cách 1 bạn đã chọn)
            await orderRepository.BeginTransactionAsync();

            try
            {
                // A. Khởi tạo Order (Chưa lưu DB ngay)
                var order = new Order(
                    customerId: request.CustomerId,
                    userId: request.UserId,
                    promoId: request.PromoId,
                    orderDate: DateTime.UtcNow.AddHours(7),
                    orderStatus: OrderStatus.Pending,
                    discountAmount: 0
                );

                // B. Xử lý từng sản phẩm
                foreach (var item in request.Items)
                {
                    // Lấy thông tin sản phẩm để lấy Giá (Price)
                    var product = await productRepository.GetById(item.ProductId);
                    if (product == null)
                    {
                        await orderRepository.RollbackTransactionAsync(); // SỬA: Gọi từ repo
                        return new ResultWithData<OrderDTO>(false, $"Sản phẩm ID {item.ProductId} không tồn tại.", null);
                    }

                    // Check Tồn kho (Inventory)
                    var inventory = await inventoryRepository.GetByProductID(item.ProductId);
                    if (inventory == null || inventory.Quantity < item.Quantity)
                    {
                        await orderRepository.RollbackTransactionAsync();
                        return new ResultWithData<OrderDTO>(false, $"Sản phẩm '{product.ProductName}' không đủ hàng (Còn: {inventory?.Quantity ?? 0}).", null);
                    }

                    // TRỪ KHO: Gọi Inventory Repository để trừ
                    await inventoryRepository.deductQuantityOfCreatedOrder(item.ProductId, item.Quantity);
                    // Lưu ý: Không cần gọi productRepository.Update(product) nếu stock nằm bên bảng Inventory

                    // Thêm vào Order
                    order.Items.Add(new OrderItem(
                        orderId: order.Id,
                        productId: item.ProductId,
                        quantity: item.Quantity,
                        price: product.Price
                    ));
                }

                // C. Tính toán giảm giá (Promotion)
                if (request.PromoId.HasValue)
                {
                    var promotion = await promotionRepository.GetById(request.PromoId.Value);

                    // Validate Promotion: Tồn tại + Còn hạn + Đang Active
                    if (promotion != null && promotion.IsActive())
                    {
                        decimal discount = 0;
                        decimal currentSubTotal = order.Items.Sum(x => x.Subtotal);

                        if (promotion.DiscountType == DiscountType.Percentage)
                        {
                            discount = currentSubTotal * (promotion.DiscountValue / 100);
                        }
                        else if (promotion.DiscountType == DiscountType.FixedAmount)
                        {
                            discount = promotion.DiscountValue;
                        }

                        discount = Math.Min(discount, currentSubTotal);
                        order.SetDiscount(discount);
                    }
                    // Nếu Promotion không hợp lệ thì bỏ qua (hoặc return lỗi tùy nghiệp vụ)
                }

                // D. Tính tổng tiền và Lưu Order
                order.CalculateTotal();
                await orderRepository.Create(order);

                // E. Tạo Payment
                // Validate PaymentMethod: Nếu null thì mặc định Cash
                var method = PaymentMethod.Cash;

                var payment = new Payment(
                    order.Id,
                    order.TotalAmount,
                    method
                );

                await paymentRepository.Create(payment);

                // F. CHỐT TRANSACTION (Commit)
                await orderRepository.CommitTransactionAsync(); // SỬA: Gọi từ repo

                return new ResultWithData<OrderDTO>(
                    true,
                    "Tạo đơn hàng thành công",
                    order.ToDTO()
                );
            }
            catch (Exception ex)
            {
                // G. HOÀN TÁC NẾU LỖI (Rollback)
                await orderRepository.RollbackTransactionAsync(); // SỬA: Gọi từ repo
                return new ResultWithData<OrderDTO>(false, "Lỗi hệ thống: " + ex.Message, null);
            }
        }
    }
}