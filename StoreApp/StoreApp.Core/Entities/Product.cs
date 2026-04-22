using StoreApp.Core.Exceptions;

namespace StoreApp.Core.Entities
{
    public class Product(Guid categoryId, Guid supplierId, string productName, decimal price, string imageUrl) : BaseEntity
    {
        public Guid CategoryId { get; private set; } = categoryId;
        public Guid SupplierId { get; private set; } = supplierId;
        public string ProductName { get; private set; } = productName;
        public decimal Price { get; private set; } = price;
        public int Quantity { get; private set; } = 0;
        public DateTime CreatedAt { get; private set; } = DateTime.Now; 
        public string ImageUrl { get; private set; } = imageUrl;
        
        public void Update(Guid categoryId, string productName, decimal price, string imageUrl)
        {
            CategoryId = categoryId;
            ProductName = productName;
            Price = price;
            ImageUrl = imageUrl;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity < 0)
                throw new InvalidStockQuantityException("Số lượng sản phẩm không được âm");
            Quantity += quantity;
        }

        public void EnsureCanBeDeleted()
        {
            if (Quantity > 0)
                throw new ProductCannotBeDeletedException("Không thể xóa sản phẩm còn tồn kho");
        }

    }
}
