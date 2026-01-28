using Microsoft.EntityFrameworkCore;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;
using System;

namespace StoreApp.Infrastructure.Adapter
{
    public class PromotionRepository(StoreDbContext context) : BaseRepository<Promotion>(context), IPromotionRepository
    {
        protected readonly DbSet<Promotion> _dbSet = context.Set<Promotion>();
        public async Task<List<Promotion>> SearchByKeyword(string keyword)
        {
            keyword = keyword.Trim().ToLower();

            return await _dbSet
                .Where(x => x.PromoCode.ToLower().Contains(keyword) ||
                            (x.Description != null && x.Description.ToLower().Contains(keyword)))
                .ToListAsync(); ;
        }
    }
}
