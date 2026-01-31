namespace StoreApp.Core.Entities
{
    public class Customer(string name, string? phone, string? email, string? address, DateTime? createdAt) : BaseEntity
    {
        // Gán trực tiếp từ tham số của Primary Constructor vào Property
        public string Name { get; private set; } = name;
        public string? Phone { get; private set; } = phone;
        public string? Email { get; private set; } = email;
        public string? Address { get; private set; } = address;
        public DateTime? CreatedAt { get; private set; } = createdAt ?? DateTime.Now;

        // EF Core vẫn cần một constructor trống để "bí mật" khởi tạo đối tượng
        private Customer() : this(default!, null, null, null, null) { }

        public void Update(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            // Lưu ý: Thường không nên cập nhật CreatedAt khi Update khách hàng
        }
    }
}
