using MediatR;
using StoreApp.Application.Repository;
using StoreApp.Application.Results;
using StoreApp.Application.UseCases.OrderUseCase.Command.Update;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject; // Nơi chứa Enum OrderStatus, PaymentStatus

namespace StoreApp.Application.UseCases.OrderUseCase.Command.UpdateStatus
{
    public class UpdateOrderStatusHandler(
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository,
        IGRNRepository inventoryRepository
    ) : IRequestHandler<UpdateOrderStatusCommand, ResultWithData<string>>
    {
        public async Task<ResultWithData<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetById(request.OrderId);
            if (order == null) return new ResultWithData<string>(false, "Không tìm thấy đơn hàng", null);

            if (request.IsSuccess)
            {
                // 1. Update Order thành PAID
                order.Update(OrderStatus.Paid);
                await orderRepository.Update(order);

                // 2. TẠO PAYMENT (Biên lai xác nhận)
                // Vì bảng Payment không có Status, nên việc tạo ra dòng này
                // đồng nghĩa với việc "Đã thanh toán thành công"
                var payment = new Payment(
                    orderId: order.Id,
                    amount: order.TotalAmount,
                    paymentMethod: PaymentMethod.VnPay
                );


                await paymentRepository.Create(payment);
            }
            else
            {
                // Nếu thất bại -> Chỉ hủy đơn, KHÔNG tạo Payment
                // 1. Chỉ hoàn kho nếu đơn hàng đang ở trạng thái Pending
                // (Tránh trường hợp Callback gọi 2 lần gây cộng dồn sai)
                if (order.OrderStatus == OrderStatus.Pending)
                {
                    // 2. Duyệt qua từng sản phẩm trong đơn hàng
                    foreach (var item in order.Items)
                    {
                        // Gọi hàm cộng lại số lượng (Bạn cần viết hàm này trong Repo)
                        await inventoryRepository.RestockQuantity(item.ProductId, item.Quantity);
                    }

                    // 3. Cập nhật trạng thái Hủy
                    order.Update(OrderStatus.Cancelled);

                    await orderRepository.Update(order);
                    // SaveChanges...
                }
            }

            return new ResultWithData<string>(true, "Cập nhật thành công", null);
        }
    }
}