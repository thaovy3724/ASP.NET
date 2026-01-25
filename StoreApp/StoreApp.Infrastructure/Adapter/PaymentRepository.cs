using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class PaymentRepository(DbContext context) : BaseRepository<Payment>(context), IPaymentRepository
    {
    }
}
