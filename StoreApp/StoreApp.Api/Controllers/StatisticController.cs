using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.StatisticUseCase.Query;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticController(IMediator mediator) : ControllerBase
    {
        [HttpGet("daily-revenue")]
        public async Task<IActionResult> GetDailyRevenue(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(
                new GetDailyRevenueStatisticQuery(fromDate, toDate),
                cancellationToken));
        }

        [HttpGet("financial")]
        public async Task<IActionResult> GetFinancial(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(
                new GetFinancialStatisticQuery(fromDate, toDate),
                cancellationToken));
        }

        [HttpGet("best-selling-products")]
        public async Task<IActionResult> GetBestSellingProducts(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            [FromQuery] int top = 10,
            CancellationToken cancellationToken = default)
        {
            return Ok(await mediator.Send(
                new GetBestSellingProductsStatisticQuery(fromDate, toDate, top),
                cancellationToken));
        }

        [HttpGet("order-status")]
        public async Task<IActionResult> GetOrderStatus(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(
                new GetOrderStatusStatisticQuery(fromDate, toDate),
                cancellationToken));
        }

        [HttpGet("low-stock-products")]
        public async Task<IActionResult> GetLowStockProducts(
            [FromQuery] int threshold = 10,
            CancellationToken cancellationToken = default)
        {
            return Ok(await mediator.Send(
                new GetLowStockProductsStatisticQuery(threshold),
                cancellationToken));
        }

        [HttpGet("payment-method-revenue")]
        public async Task<IActionResult> GetPaymentMethodRevenue(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(
                new GetPaymentMethodRevenueStatisticQuery(fromDate, toDate),
                cancellationToken));
        }

        [HttpGet("category-revenue")]
        public async Task<IActionResult> GetCategoryRevenue(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate,
            CancellationToken cancellationToken)
        {
            return Ok(await mediator.Send(
                new GetCategoryRevenueStatisticQuery(fromDate, toDate),
                cancellationToken));
        }
    }
}