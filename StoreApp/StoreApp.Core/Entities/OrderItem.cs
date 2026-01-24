using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreApp.Core.Entities
{
    [Table("order_items")]
    public class OrderItem
    {
        [Key]
        [Column("order_item_id")]
        public int OrderItemId { get; set; }
        [Column("order_id")]
        public int? OrderId { get; set; }
        [Column("product_id")]
        public int? ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("subtotal")]
        public decimal Subtotal { get; set; }

        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
