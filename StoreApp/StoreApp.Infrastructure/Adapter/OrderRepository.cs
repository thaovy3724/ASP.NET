using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class OrderRepository(StoreDbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        private IDbContextTransaction _currentTransaction;

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


        public async Task BeginTransactionAsync()
        {
            // Bắt đầu 1 transaction từ DbContext hiện tại
            _currentTransaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await context.SaveChangesAsync(); // Lưu tất cả thay đổi đang chờ
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public Task<Order?> GetOrderWithDetails(Guid id)
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<List<Order>> GetListOrderWithDetails()
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .ToListAsync();
        }

        public Task<List<Order>> GetListExpiredOrders(DateTime timeLimit)
        { // kiểm tra các đơn hàng Pending và chưa quá hạn
            return DbSet.AsNoTracking()
                        .Where(o => o.OrderStatus == OrderStatus.Pending && o.UpdatedAt < timeLimit)
                        .ToListAsync();
        }

        public Task<bool> HasProductReference(Guid productId)
        {
            return DbSet.AsNoTracking()
                        .Include(o => o.Items)
                        .AnyAsync(o => o.Items.Any(oi => oi.ProductId == productId));
        }
    }
}
