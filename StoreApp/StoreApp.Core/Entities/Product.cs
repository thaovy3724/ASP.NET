namespace StoreApp.Core.Entities
{
    public class Product(Guid categoryId, Guid supplierId, string productName, string barcode, decimal price, string unit, string? imageUrl = null) : BaseEntity
    {
        public Guid CategoryId { get; private set; } = categoryId;
        public Guid SupplierId { get; private set; } = supplierId;
        public string ProductName { get; private set; } = productName;
        public string Barcode { get; private set; } = barcode;
        public decimal Price { get; private set; } = price;
        public string Unit { get; private set; } = unit;

        public DateTime CreatedAt { get; private set; } = DateTime.Now; 
        public string? ImageUrl { get; private set; } = imageUrl;
        


        public void Update(Guid categoryId, Guid supplierId, string productName, string barcode, decimal price, string unit, string? imageUrl)
        {
            CategoryId = categoryId;
            SupplierId = supplierId;
            ProductName = productName;
            Barcode = barcode;
            Price = price;
            Unit = unit;
            ImageUrl = imageUrl;
        }
    }
}
