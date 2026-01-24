namespace StoreApp.Core.Entities
{
    public class Product : BaseEntity
    {
        public Guid CategoryID { get; private set; }
        public Guid SupplierID { get; private set; }
        public string ProductName { get; private set; } = "";
        public string Barcode { get; private set; } = "";
        public decimal Price { get; private set; }
        public string Unit { get; private set; } = "";
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public string ImageUrl { get; private set; } = "";
        //public int Status { get; set; }
    }
}
