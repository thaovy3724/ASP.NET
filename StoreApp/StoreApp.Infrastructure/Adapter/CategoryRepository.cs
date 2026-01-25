using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Ports.Output;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public class CategoryRepository(DbContext context) : BaseRepository<Category>(context), ICategoryRepository
    {
    }
}
