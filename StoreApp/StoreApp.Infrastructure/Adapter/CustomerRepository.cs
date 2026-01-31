using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class CustomerRepository(StoreDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
        public async Task<List<Customer>> SearchByKeyword(string? keyword)
        {
            // 1. Khởi tạo query từ DbSet của Customer
            var query = context.Set<Customer>().AsQueryable();

            // 2. Kiểm tra nếu keyword không trống thì mới lọc
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                // Giả sử Entity Customer có trường Name và Email để tìm kiếm
                // Bạn có thể tùy chỉnh theo các thuộc tính thực tế của Customer
                query = query.Where(c => c.Name.Contains(keyword) ||
                                         c.Email.Contains(keyword) ||
                                         c.Phone.Contains(keyword));
            }

            // 3. Thực thi truy vấn và trả về danh sách
            return await query.AsNoTracking().ToListAsync();
        }

        public Task<bool> IsEmailExists(string email)
        {
            return context.Set<Customer>()
                          .AsNoTracking()
                          .AnyAsync(c => c.Email == email);
        }
        public Task<bool> IsPhoneExists(string phone)
        {
            return context.Set<Customer>()
                          .AsNoTracking()
                          .AnyAsync(c => c.Phone == phone);
        }
        public Task<bool> IsExistOderOfCustomer(Guid customerId)
        {
            return context.Set<Order>()
                          .AsNoTracking()
                          .AnyAsync(o => o.CustomerId == customerId);
        }
    }
}
