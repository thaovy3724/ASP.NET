using StoreApp.Core.Exceptions;
using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Order(
        Guid customerId,
        Guid? staffId,
        DateTime updatedAt,
        string address,
        PaymentMethod paymentMethod,
        OrderStatus orderStatus) : BaseEntity
    {
        public Guid CustomerId { get; private set; } = customerId;
        public Guid? StaffId { get; private set; } = staffId;
        public DateTime UpdatedAt { get; private set; } = updatedAt;
        public OrderStatus OrderStatus { get; private set; } = orderStatus;
        public string Address { get; private set; } = address;
        public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
        public decimal TotalAmount { get; private set; }

        // Đảm bảo list không bị null
        public List<OrderDetail> Items { get; private set; } = [];

        public void Update( OrderStatus status)
        {
            OrderStatus = status;
        }

        public void MarkAsConfirmed()
        {
            if(OrderStatus != OrderStatus.Pending)
            {
                throw new OrderCannotBeConfirmedException("Chỉ có thể xác nhận đơn hàng ở trạng thái Pending.");
            }
            OrderStatus = OrderStatus.Confirmed;
        }

        public void MarkAsDelivered()
        {
            if (OrderStatus != OrderStatus.Confirmed)
            {
                throw new OrderCannotBeDeliveredException("Chỉ có thể giao hàng cho đơn hàng đã được xác nhận.");
            }
            OrderStatus = OrderStatus.Delivered;
        }

        public void CancelOrder()
        {
            if (OrderStatus != OrderStatus.Confirmed)
            {
                throw new OrderCannotBeCanceledException("Không thể hủy đơn hàng đã được xác nhận.");
            }
            OrderStatus = OrderStatus.Canceled;
        }

        //public void CalculateTotal()
        //{
        //    var subTotal = Items.Sum(x => x.Subtotal);
        //    TotalAmount = subTotal - DiscountAmount;
        //    if (TotalAmount < 0) TotalAmount = 0;
        //}
    }
}