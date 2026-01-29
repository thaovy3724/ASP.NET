using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Promotion : BaseEntity
    {
        public string PromoCode { get; private set; } = "";
        public string? Description { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public decimal DiscountValue { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}
