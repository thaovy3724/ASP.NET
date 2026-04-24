using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Entities
{
    public class Voucher(decimal discountPercent,decimal maxDiscountAmount, DateTime startDate, DateTime endDate, int quantity, bool status) : BaseEntity
    {
        public decimal DiscountPercent { get; private set; } = discountPercent;
        public decimal MaxDiscountAmount { get; private set; } = maxDiscountAmount;
        public DateTime StartDate { get; private set; } = startDate;
        public DateTime EndDate { get; private set; } = endDate;
        public int Quantity { get; private set; } = quantity;
        public bool Status { get; private set; } = status;
        public void decreaseQuantity()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }
        }
    }
}
