using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Entities
{
    [Table("imports")]
    public class Import
    {
        [Key]
        [Column("import_id")]
        public int ImportId { get; set; }

        [Required]
        [Column("import_date")]
        public DateTime ImportDate { get; set; }

        [Required]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("total_amount", TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? Staff { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier? Supplier { get; set; }

        public ICollection<ImportDetail>? ImportDetails { get; set; }
    }
}
