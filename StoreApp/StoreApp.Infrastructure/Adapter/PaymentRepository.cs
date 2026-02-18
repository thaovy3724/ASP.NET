using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class PaymentRepository(StoreDbContext context) : BaseRepository<Payment>(context), IPaymentRepository
    {
        public Task<Payment> GetByOrderId(Guid orderId)
        {
            return context.Set<Payment>().FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public Task<List<Payment>> SearchByKeyword(string keyword)
        {
            var query = context.Set<Payment>().AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.OrderId.ToString().Contains(keyword) ||
                                         p.Amount.ToString().Contains(keyword) ||
                                         p.PaymentMethod.ToString().Contains(keyword));
            }
            return query.ToListAsync();
        }
    }
}
