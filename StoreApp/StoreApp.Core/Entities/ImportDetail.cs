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
    [Table("import_details")]
    public class ImportDetail
    {
        [Key]
        [Column("import_detail_id")]
        public int ImportDetailId { get; set; }

        [Required]
        [Column("import_id")]
        public int ImportId { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Subtotal { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ImportId))]
        public Import Import { get; set; }

    }
}
