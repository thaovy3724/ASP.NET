using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Adapter
{
    public class SupplierRepository(StoreDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
    {
        private readonly DbSet<Supplier> _dbset = context.Set<Supplier>();
        private readonly DbSet<Product> _dbsetProduct = context.Set<Product>();
        public async Task<List<Supplier>> SearchByKeyword(string keyword)
        {
            var k = keyword.Trim().ToLower(); // Chuyển từ khóa về chữ thường

            return await _dbset
                .Where(x =>
                    (x.Name != null && x.Name.ToLower().Contains(k)) ||
                    (x.Email != null && x.Email.ToLower().Contains(k)) ||
                    (x.Phone != null && x.Phone.ToLower().Contains(k)) ||
                    (x.Address != null && x.Address.ToLower().Contains(k)))
                .ToListAsync();
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
        public async Task<bool> IsSupplierNameExist(string name, Guid? ignoreId = null)
        {
            name = name?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return await q.AnyAsync(x => x.Name == name);
        }
        public async Task<bool> IsSupplierEmailExist(string email, Guid? ignoreId = null)
        {
            email = email?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return await q.AnyAsync(x => x.Email == email);
        }
        public async Task<bool> IsSupplierPhoneExist(string phone, Guid? ignoreId = null)
        {
            phone = phone?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return await q.AnyAsync(x => x.Phone == phone);
        }
        public async Task<bool> IsSupplierAddressExist(string address, Guid? ignoreId = null)
        {
            address = address?.Trim() ?? "";
            var q = _dbset.AsQueryable();
            if (ignoreId.HasValue) q = q.Where(x => x.Id != ignoreId.Value);
            return await q.AnyAsync(x => x.Address == address);
        }
        public async Task<bool> IsExistProductOfSupplier(Guid Id)
        {
            return await _dbsetProduct.AsNoTracking()
                .AsNoTracking()
                .AnyAsync(x => x.SupplierId == Id);
        }

    }
}
