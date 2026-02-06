using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Promotion(string? promoCode, string? description, DiscountType discountType, decimal discountValue, DateTime startDate, DateTime endDate) : BaseEntity
    {
        public string PromoCode { get; private set; } = promoCode ?? string.Empty;
        public string? Description { get; private set; } = description;
        public DiscountType DiscountType { get; private set; } = discountType;
        public decimal DiscountValue { get; private set; } = discountValue;
        public DateTime StartDate { get; private set; } = startDate;
        public DateTime EndDate { get; private set; } = endDate;

        public void Update(string? promoCode, string? description, DiscountType discountType, decimal discountValue, DateTime startDate, DateTime endDate)
        {
            PromoCode = promoCode ?? PromoCode;
            Description = description ?? Description;
            DiscountType = discountType;
            DiscountValue = discountValue;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsActive()
        {
            var now = DateTime.UtcNow;
            return now >= StartDate && now <= EndDate;
        }
    }
}
