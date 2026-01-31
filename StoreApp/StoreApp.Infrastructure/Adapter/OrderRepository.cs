using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class OrderRepository(StoreDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<List<Order>> Search(string? keyword)
        {
            // 1. Dùng AsNoTracking để tăng tốc độ (vì đây là lệnh Query, không phải Update)
            // 2. Include các bảng liên quan để DTO có đủ dữ liệu
            var query = DbSet.AsNoTracking()
                             .Include(o => o.Items)
                             .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                // Kiểm tra xem keyword có phải là một GUID hợp lệ không
                bool isGuid = Guid.TryParse(keyword, out Guid guidKeyword);

                query = query.Where(o =>
                    (isGuid && (o.Id == guidKeyword || o.CustomerId == guidKeyword)) || // Tìm chính xác theo ID
                    o.OrderStatus.ToString().Contains(keyword) // Tìm theo trạng thái
                                                               // Bạn có thể thêm tìm kiếm theo tên khách hàng nếu đã Include Customer
                );
            }

            return await query.ToListAsync();
        }
    }
}
