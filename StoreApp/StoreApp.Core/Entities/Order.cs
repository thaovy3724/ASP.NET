using StoreApp.Core.Exceptions;
using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Order(Guid customerId,string address, PaymentMethod paymentMethod) : BaseEntity
    {
        public Guid CustomerId { get; private set; } = customerId;
        public Guid? StaffId { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public string Address { get; private set; } = address;
        public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
        public decimal TotalAmount => Items.Sum(x => x.Subtotal);

        // Đảm bảo list không bị null
        public List<OrderDetail> Items { get; private set; } = [];

        public void Update( OrderStatus status)
        {
            OrderStatus = status;
        }

        public void ConfirmOrder(Guid staffId)
        {
            var canConfirm =
                (OrderStatus == OrderStatus.Pending && PaymentMethod == PaymentMethod.Cash)
                || (OrderStatus == OrderStatus.Paid && PaymentMethod == PaymentMethod.VnPay);

            if (!canConfirm)
                throw new OrderCannotBeConfirmedException("Không thể xác nhận đơn hàng do trạng thái đơn hàng không hợp lệ");

            StaffId = staffId;
            OrderStatus = OrderStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeliverOrder(Guid staffId)
        {
            if (OrderStatus != OrderStatus.Confirmed)
            {
                throw new OrderCannotBeDeliveredException("Chỉ có thể giao hàng cho đơn hàng đã được xác nhận.");
            }
            StaffId = staffId;
            OrderStatus = OrderStatus.Delivered;
            UpdatedAt = DateTime.UtcNow;
        }

        public void CancelOrder(Guid? staffId = null)
        {
            var canCancel =
                // Cash: staff và customer đều huỷ khi Pending
                (PaymentMethod == PaymentMethod.Cash && OrderStatus == OrderStatus.Pending)

                // VNPay: customer / auto-cancel / callback fail => huỷ khi Pending
                || (PaymentMethod == PaymentMethod.VnPay && staffId is null && OrderStatus == OrderStatus.Pending)

                // VNPay: staff huỷ khi Paid
                || (PaymentMethod == PaymentMethod.VnPay && staffId is not null && OrderStatus == OrderStatus.Paid);

            if (!canCancel)
                
            {
                throw new OrderCannotBeCanceledException("Không thể hủy đơn hàng ở trạng thái hiện tại.");
            }

            if(staffId is not null) // Nếu khách hàng hủy đơn hàng thì staffId sẽ là null, ngược lại nếu nhân viên hủy đơn hàng thì sẽ có staffId
            {
                StaffId = staffId.Value;
            }

            OrderStatus = OrderStatus.Canceled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void PayOrder()
        {
            if(OrderStatus == OrderStatus.Pending && PaymentMethod == PaymentMethod.VnPay)
            {
                OrderStatus = OrderStatus.Paid;
                UpdatedAt = DateTime.UtcNow;
            }
            else throw new OrderCannotBePaidException("Không thể thanh toán cho đơn hàng do trạng thái đơn hàng không hợp lệ.");
        }
        public void AddItem(Guid productId, int quantity, decimal price)
        {
            var item = new OrderDetail(Id, productId, quantity, price);
            Items.Add(item);
        }
    }
}