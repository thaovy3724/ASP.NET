using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("customer_id")]
        public int? CustomerId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("promo_id")]
        public int? PromoId { get; set; }

        [Column("order_date")]
        public DateTime? OrderDate { get; set; }

        [Column("total_amount")]
        public decimal? TotalAmount { get; set; }

        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }

        public List<OrderItem> Items { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
    }
}
