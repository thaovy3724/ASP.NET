using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class PaymentRepository(StoreDbContext context) : BaseRepository<Payment>(context), IPaymentRepository
    {
    }
}
