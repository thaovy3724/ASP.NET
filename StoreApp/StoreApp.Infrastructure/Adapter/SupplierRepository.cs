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
        public Task<bool> IsSupplierNameExist(string name, Guid? ignoreId = null)
        {
            name = name?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return q.AnyAsync(x => x.Name == name);
        }
        public Task<bool> IsSupplierEmailExist(string email, Guid? ignoreId = null)
        {
            email = email?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return q.AnyAsync(x => x.Email == email);
        }
        public Task<bool> IsSupplierPhoneExist(string phone, Guid? ignoreId = null)
        {
            phone = phone?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return q.AnyAsync(x => x.Phone == phone);
        }
        public Task<bool> IsSupplierAddressExist(string address, Guid? ignoreId = null)
        {
            address = address?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return q.AnyAsync(x => x.Address == address);
        }

    }
}
