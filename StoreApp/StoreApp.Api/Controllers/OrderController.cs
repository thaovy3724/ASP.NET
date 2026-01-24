using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderApplicationService _service;
        //private readonly ILogger<OrderController> _logger;

        public DonHangController(IDonHangService service)
        {
            _service = service;
            //_logger = logger;
        }

        // GET /api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAll();
                var resp = new ApiResponse<List<DonHangDTO>>
                {
                    Success = true,
                    Message = "Lấy danh sách đơn hàng thành công",
                    DataDTO = data
                };
                return Ok(resp); // 200
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET /api/orders failed");
                var resp = new ApiResponse<List<DonHangDTO>>
                {
                    Success = false,
                    Message = "Đã xảy ra lỗi hệ thống khi lấy danh sách đơn hàng.",
                    DataDTO = null
                };
                return StatusCode(500, resp); // 500
            }
        }

    }
}
