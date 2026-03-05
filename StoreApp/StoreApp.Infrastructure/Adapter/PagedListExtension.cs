using Microsoft.EntityFrameworkCore;
using StoreApp.Core.Entities;

namespace StoreApp.Infrastructure.Adapter
{
    public static class PagedListExtension
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> source, int pageNumber, int pageSize)
        {
            // Bước 1: Count trực tiếp tại DB (chỉ trả về 1 con số, cực nhẹ RAM)
            var count = await source.CountAsync();

            // Bước 2: Skip/Take tại DB (chỉ lấy đúng số dòng cần thiết lên RAM)
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
