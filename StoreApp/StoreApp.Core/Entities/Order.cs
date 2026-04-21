using StoreApp.Core.Exceptions;
using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Order(Guid customerId, Guid? addressId, string address, PaymentMethod paymentMethod) : BaseEntity
    {
        public Guid CustomerId { get; private set; } = customerId;
        public Guid? StaffId { get; private set; }
        public Guid? AddressId { get; private set; } = addressId;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.AddHours(7);
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
        public string Address { get; private set; } = address;
        public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
        public decimal TotalAmount => Items.Sum(x => x.Subtotal);

        public List<OrderDetail> Items { get; private set; } = [];

        public void Update(OrderStatus status)
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
            UpdatedAt = DateTime.UtcNow.AddHours(7);
        }

        public void DeliverOrder(Guid staffId)
        {
            if (OrderStatus != OrderStatus.Confirmed)
            {
                throw new OrderCannotBeDeliveredException("Chỉ có thể giao hàng cho đơn hàng đã được xác nhận.");
            }
            StaffId = staffId;
            OrderStatus = OrderStatus.Delivered;
            UpdatedAt = DateTime.UtcNow.AddHours(7);
        }

        public void CancelOrder(Guid? staffId = null)
        {
            var canCancel =
                (PaymentMethod == PaymentMethod.Cash && OrderStatus == OrderStatus.Pending)
                || (PaymentMethod == PaymentMethod.VnPay && staffId is null && OrderStatus == OrderStatus.Pending)
                || (PaymentMethod == PaymentMethod.VnPay && staffId is not null && OrderStatus == OrderStatus.Paid);

            if (!canCancel)
            {
                throw new OrderCannotBeCanceledException("Không thể hủy đơn hàng ở trạng thái hiện tại.");
            }

            if (staffId is not null)
            {
                StaffId = staffId.Value;
            }

            OrderStatus = OrderStatus.Canceled;
            UpdatedAt = DateTime.UtcNow.AddHours(7);
        }

        public void PayOrder()
        {
            if (OrderStatus == OrderStatus.Pending && PaymentMethod == PaymentMethod.VnPay)
            {
                OrderStatus = OrderStatus.Paid;
                UpdatedAt = DateTime.UtcNow.AddHours(7);
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
