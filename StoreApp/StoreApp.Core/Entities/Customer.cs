using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Entities
{
    [Table("customers")]
    public class Customer
    {
        [Key] // khóa chính
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; } = "";

        [Column("phone", TypeName = "varchar(20)")]
        public string? Phone { get; set; }

        [Column("email", TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Column("reward_points", TypeName = "int")]
        public int RewardPoints { get; set; } = 0;
    }
}
