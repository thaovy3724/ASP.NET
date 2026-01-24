using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Entities
{
    [Table("categories")]
    public class Category
    {
        [Key] // khóa chính
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên loại sản phẩm không được trống")]
        [Column("category_name", TypeName = "varchar(100")]
        public string CategoryName { get; set; } = "";

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
