using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class PromotionRepository(DbContext context) : BaseRepository<Promotion>(context), IPromotionRepository
    {
    }
}
