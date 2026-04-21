using StoreApp.Core.Exceptions;
using StoreApp.Core.ValueObject;

namespace StoreApp.Core.Entities
{
    public class Voucher(
        string code,
        decimal discountPercent,
        decimal maxDiscountAmount,
        DateTime startDate,
        DateTime endDate,
        int quantity,
        VoucherStatus status = VoucherStatus.Active
    ) : BaseEntity
    {
        public string Code { get; private set; } = code.Trim().ToUpper();
        public decimal DiscountPercent { get; private set; } = discountPercent;
        public decimal MaxDiscountAmount { get; private set; } = maxDiscountAmount;
        public DateTime StartDate { get; private set; } = startDate;
        public DateTime EndDate { get; private set; } = endDate;
        public int Quantity { get; private set; } = quantity;
        public VoucherStatus Status { get; private set; } = status;

        public void Update(
            string code,
            decimal discountPercent,
            decimal maxDiscountAmount,
            DateTime startDate,
            DateTime endDate,
            int quantity,
            VoucherStatus status)
        {
            Code = code.Trim().ToUpper();
            DiscountPercent = discountPercent;
            MaxDiscountAmount = maxDiscountAmount;
            StartDate = startDate;
            EndDate = endDate;
            Quantity = quantity;
            Status = status;
        }

        public bool CanUse(DateTime now)
        {
            return Status == VoucherStatus.Active
                   && Quantity > 0
                   && StartDate <= now
                   && EndDate >= now;
        }

        public decimal CalculateDiscount(decimal totalAmount)
        {
            var discount = totalAmount * DiscountPercent / 100m;
            return Math.Min(discount, MaxDiscountAmount);
        }

        public void Inactivate()
        {
            Status = VoucherStatus.Inactive;
        }

        public void EnsureUsable(DateTime now)
        {
            if (!CanUse(now))
                throw new VoucherCannotBeUsedException("Voucher không hợp lệ, đã hết hạn hoặc đã hết số lượng.");
        }
    }
}