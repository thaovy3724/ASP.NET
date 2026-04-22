using Microsoft.EntityFrameworkCore;
using StoreApp.Application.DTOs;
using StoreApp.Application.Repository;
using StoreApp.Core.Entities;
using StoreApp.Core.ValueObject;
using StoreApp.Infrastructure.Data;

namespace StoreApp.Infrastructure.Adapter
{
    public class StatisticRepository(StoreDbContext context) : IStatisticRepository
    {
        private IQueryable<Order> ValidSalesOrders(DateTime fromDate, DateTime toDate)
        {
            var from = fromDate.Date;
            var toExclusive = toDate.Date.AddDays(1);

            return context.Set<Order>()
                .AsNoTracking()
                .Where(o =>
                    o.UpdatedAt >= from &&
                    o.UpdatedAt < toExclusive &&
                    (o.OrderStatus == OrderStatus.Paid ||
                     o.OrderStatus == OrderStatus.Delivered));
        }

        public async Task<List<DailyRevenueStatisticDTO>> GetDailyRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var orders = await ValidSalesOrders(fromDate, toDate)
                .Include(o => o.Items)
                .ToListAsync(cancellationToken);

            return orders
                .GroupBy(o => o.UpdatedAt.Date)
                .Select(g => new DailyRevenueStatisticDTO(
                    g.Key,
                    g.Sum(o => o.Items.Sum(i => i.Quantity * i.Price)),
                    g.Count()
                ))
                .OrderBy(x => x.Date)
                .ToList();
        }

        public async Task<List<FinancialStatisticDTO>> GetFinancialStatisticAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var from = fromDate.Date;
            var toExclusive = toDate.Date.AddDays(1);

            var orderRows = await ValidSalesOrders(fromDate, toDate)
                .Select(o => new
                {
                    Date = o.UpdatedAt.Date,
                    Revenue = o.Items.Sum(i => i.Quantity * i.Price)
                })
                .GroupBy(x => x.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    OrderRevenue = g.Sum(x => x.Revenue),
                    OrderCount = g.Count()
                })
                .ToListAsync(cancellationToken);

            var grnRows = await context.Set<GRN>()
                .AsNoTracking()
                .Where(g =>
                    g.UpdatedAt >= from &&
                    g.UpdatedAt < toExclusive &&
                    g.Status == GRNStatus.Completed)
                .Select(g => new
                {
                    Date = g.UpdatedAt.Date,
                    Cost = g.Items.Sum(i => i.Quantity * i.Price)
                })
                .GroupBy(x => x.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    GRNCost = g.Sum(x => x.Cost)
                })
                .ToListAsync(cancellationToken);

            var orderByDate = orderRows.ToDictionary(x => x.Date);
            var grnByDate = grnRows.ToDictionary(x => x.Date);

            var totalDays = (toDate.Date - fromDate.Date).Days + 1;

            return Enumerable.Range(0, totalDays)
                .Select(i =>
                {
                    var date = fromDate.Date.AddDays(i);

                    var orderRevenue = orderByDate.TryGetValue(date, out var order)
                        ? order.OrderRevenue
                        : 0m;

                    var orderCount = orderByDate.TryGetValue(date, out var orderCountRow)
                        ? orderCountRow.OrderCount
                        : 0;

                    var grnCost = grnByDate.TryGetValue(date, out var grn)
                        ? grn.GRNCost
                        : 0m;

                    return new FinancialStatisticDTO(
                        date,
                        orderRevenue,
                        grnCost,
                        orderRevenue - grnCost,
                        orderCount
                    );
                })
                .ToList();
        }

        public async Task<List<BestSellingProductStatisticDTO>> GetBestSellingProductsAsync(
            DateTime fromDate,
            DateTime toDate,
            int top,
            CancellationToken cancellationToken = default)
        {
            var orderIds = await ValidSalesOrders(fromDate, toDate)
                .Select(o => o.Id)
                .ToListAsync(cancellationToken);

            if (!orderIds.Any())
            {
                return new List<BestSellingProductStatisticDTO>();
            }

            var rows = await context.Set<OrderDetail>()
                .AsNoTracking()
                .Where(od => orderIds.Contains(od.OrderId))
                .Join(context.Set<Product>().AsNoTracking(),
                    od => od.ProductId,
                    p => p.Id,
                    (od, p) => new
                    {
                        ProductId = p.Id,
                        ProductName = p.ProductName,
                        Quantity = od.Quantity,
                        Price = od.Price
                    })
                .ToListAsync(cancellationToken);

            return rows
                .GroupBy(x => new { x.ProductId, x.ProductName })
                .Select(g => new BestSellingProductStatisticDTO(
                    g.Key.ProductId,
                    g.Key.ProductName,
                    g.Sum(x => x.Quantity),
                    g.Sum(x => x.Quantity * x.Price)
                ))
                .OrderByDescending(x => x.TotalQuantitySold)
                .ThenByDescending(x => x.TotalRevenue)
                .Take(top)
                .ToList();
        }

        public async Task<List<OrderStatusStatisticDTO>> GetOrderStatusStatisticAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var from = fromDate.Date;
            var toExclusive = toDate.Date.AddDays(1);

            var rows = await context.Set<Order>()
                .AsNoTracking()
                .Where(o => o.UpdatedAt >= from && o.UpdatedAt < toExclusive)
                .GroupBy(o => o.OrderStatus)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(cancellationToken);

            return rows
                .Select(x => new OrderStatusStatisticDTO(
                    x.Status.ToString(),
                    x.Count
                ))
                .ToList();
        }

        public async Task<List<LowStockProductStatisticDTO>> GetLowStockProductsAsync(
            int threshold,
            CancellationToken cancellationToken = default)
        {
            return await context.Set<Product>()
                .AsNoTracking()
                .Where(p => p.Quantity <= threshold)
                .OrderBy(p => p.Quantity)
                .Select(p => new LowStockProductStatisticDTO(
                    p.Id,
                    p.ProductName,
                    p.Quantity
                ))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PaymentMethodRevenueStatisticDTO>> GetPaymentMethodRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var validOrders = ValidSalesOrders(fromDate, toDate);

            var rows = await validOrders
                .Select(o => new
                {
                    o.PaymentMethod,
                    Revenue = o.Items.Sum(i => i.Quantity * i.Price)
                })
                .GroupBy(x => x.PaymentMethod)
                .Select(g => new
                {
                    PaymentMethod = g.Key,
                    TotalRevenue = g.Sum(x => x.Revenue),
                    OrderCount = g.Count()
                })
                .ToListAsync(cancellationToken);

            return rows
                .Select(x => new PaymentMethodRevenueStatisticDTO(
                    x.PaymentMethod.ToString(),
                    x.TotalRevenue,
                    x.OrderCount
                ))
                .ToList();
        }

        public async Task<List<CategoryRevenueStatisticDTO>> GetCategoryRevenueAsync(
            DateTime fromDate,
            DateTime toDate,
            CancellationToken cancellationToken = default)
        {
            var orderIds = await ValidSalesOrders(fromDate, toDate)
                .Select(o => o.Id)
                .ToListAsync(cancellationToken);

            if (!orderIds.Any())
            {
                return new List<CategoryRevenueStatisticDTO>();
            }

            var rows = await context.Set<OrderDetail>()
                .AsNoTracking()
                .Where(od => orderIds.Contains(od.OrderId))
                .Join(context.Set<Product>().AsNoTracking(),
                    od => od.ProductId,
                    p => p.Id,
                    (od, p) => new
                    {
                        ProductId = p.Id,
                        ProductName = p.ProductName,
                        CategoryId = p.CategoryId,
                        Quantity = od.Quantity,
                        Price = od.Price
                    })
                .Join(context.Set<Category>().AsNoTracking(),
                    x => x.CategoryId,
                    c => c.Id,
                    (x, c) => new
                    {
                        CategoryId = c.Id,
                        CategoryName = c.Name,
                        Quantity = x.Quantity,
                        Price = x.Price
                    })
                .ToListAsync(cancellationToken);

            return rows
                .GroupBy(x => new { x.CategoryId, x.CategoryName })
                .Select(g => new CategoryRevenueStatisticDTO(
                    g.Key.CategoryId,
                    g.Key.CategoryName,
                    g.Sum(x => x.Quantity),
                    g.Sum(x => x.Quantity * x.Price)
                ))
                .OrderByDescending(x => x.TotalRevenue)
                .ToList();
        }
    }
}