using MediatR;
using StoreApp.Application.DTOs;
using StoreApp.Application.Mapper;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Application.UseCases.OrderUseCase.Command.Create
{
    public class CreateOrderHandler(
IOrderRepository orderRepository,
IPaymentRepository paymentRepository) // 1. Inject thêm Payment Repository
: IRequestHandler<CreateOrderCommand, ResultWithData<OrderDTO>>
    {
        public async Task<ResultWithData<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Giả sử bạn lấy context từ Repository hoặc qua UnitOfWork để dùng Transaction
            // Ở đây mình minh họa logic nghiệp vụ:

            // 1. Tạo Entity Order
            var order = new Order(
                customerId: request.CustomerId,
                userId: request.UserId,
                promoId: request.PromoId, // Bạn nên nhận từ request nếu có
                orderDate: DateTime.UtcNow,
                orderStatus: Core.ValueObject.OrderStatus.Pending,
                discountAmount: 0
            );

            foreach (var item in request.Items)
            {
                order.Items.Add(item.ToEntity());
            }

            // 2. Tính toán tổng tiền (nếu Entity Order chưa tự tính)
            // order.CalculateTotal(); 

            // 3. Thực hiện lưu Order
            await orderRepository.Create(order);

            // 4. TỰ ĐỘNG TẠO PAYMENT tương ứng
            var payment = new Payment
            (
                order.Id,         // Lấy ID của Order vừa tạo
                order.TotalAmount, // Số tiền cần thanh toán
                Core.ValueObject.PaymentMethod.Cash      // Hoặc lấy từ request.PaymentMethod
            );

            await paymentRepository.Create(payment);

            return new ResultWithData<OrderDTO>(
                true,
                "Tạo đơn hàng và bản ghi thanh toán thành công",
                order.ToDTO()
            );
        }
    }
}
