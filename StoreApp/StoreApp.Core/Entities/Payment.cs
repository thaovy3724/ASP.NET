using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public DateTime PaymentDate { get; private set; } = DateTime.Now;
    }
}
