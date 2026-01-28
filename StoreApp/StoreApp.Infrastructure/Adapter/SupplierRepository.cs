using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class SupplierRepository(StoreDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
    {
        private readonly DbSet<Supplier> _dbset = context.Set<Supplier>();
        public List<Supplier> SearchByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<Supplier>();

            var k = keyword.Trim();
            return _dbset
                .Where(x =>
                    x.Name.Contains(k) ||
                    x.Email.Contains(k) ||
                    x.Phone.Contains(k) ||
                    x.Address.Contains(k))
                .ToList();
        }
        public async Task<bool> IsSupplierIdExist(Guid supplierId)
        {
            return await _dbset.AnyAsync(x => x.Id == supplierId);
        }

        public async Task<bool> IsSupplierExist(string name, string email, string phone, Guid? ignoreId = null)
        {
            name = name?.Trim() ?? "";
            email = email?.Trim() ?? "";
            phone = phone?.Trim() ?? "";

            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);

            return await q.AnyAsync(x =>
                (name != "" && x.Name == name) ||
                (email != "" && x.Email == email) ||
                (phone != "" && x.Phone == phone));
        }
    }
}
