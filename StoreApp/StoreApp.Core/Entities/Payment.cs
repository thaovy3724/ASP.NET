using StoreApp.Core.ValueObject;
using System.Net.NetworkInformation;

namespace StoreApp.Core.Entities
{
    public class Payment(Guid orderId, decimal amount, PaymentMethod paymentMethod) : BaseEntity
    {
        public Guid OrderId { get; private set; } = orderId;
        public decimal Amount { get; private set; } = amount;
        public PaymentMethod PaymentMethod { get; private set; } = paymentMethod;
        public DateTime PaymentDate { get; private set; } = DateTime.Now;
    }
}
