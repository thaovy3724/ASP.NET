using StoreApp.Core.Entities;

namespace StoreApp.Application.Repository
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        Task<PagedList<Voucher>> Search(
            int pageNumber,
            int pageSize,
            string? keyword = null);

        Task<Voucher?> GetByCode(string code);

        Task<bool> IsCodeExist(string code, Guid? exceptId = null);

        Task<bool> TryDecreaseQuantity(Guid voucherId, DateTime now);

        Task<List<Voucher>> GetExpiredActiveVouchers(DateTime now);
        Task<List<Voucher>> GetAvailableVouchers(DateTime now);
    }
}