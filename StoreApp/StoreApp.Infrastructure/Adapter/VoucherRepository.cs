
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Infrastructure.Adapter
{
    public class VoucherRepository(StoreDbContext dbContext) : BaseRepository<Voucher>(dbContext), IVoucherRepository
    {

    }
}
