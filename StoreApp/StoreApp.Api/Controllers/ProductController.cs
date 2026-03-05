using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.ProductUseCase.Command.Create;
using StoreApp.Application.UseCases.ProductUseCase.Command.Delete;
using StoreApp.Application.UseCases.ProductUseCase.Command.Update;
using StoreApp.Application.UseCases.ProductUseCase.Query.GetList;
using StoreApp.Application.UseCases.ProductUseCase.Query.GetOne;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator, IWebHostEnvironment env) : Controller
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductQuery(id);      // Use case của tầng Application
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListProductQuery cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
        {
            // Không cho client gửi ProductId trong body => server luôn lấy id từ route
            command = command with { Id = id };
            await mediator.Send(command);
            return NoContent(); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand(id);
            await mediator.Send(command);
            return NoContent();
        }

        // Upload image:
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            // Nếu không có file -> trả về 400 BadRequest
            if (file is null || file.Length == 0)
                return BadRequest(new { message = "File is empty." });

            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            // Lấy phần đuôi file (extension) và chuẩn hoá về chữ thường để so sánh
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            // Nếu extension không nằm trong danh sách allowed -> trả về 400 BadRequest
            if (!allowed.Contains(ext))
                return BadRequest(new { message = "Only jpg/jpeg/png/webp allowed." });

            // env.WebRootPath thường là đường dẫn tới thư mục wwwroot của project API
            // => lưu file vào: wwwroot/uploads/products
            var uploadsRoot = Path.Combine(env.WebRootPath ?? "wwwroot", "uploads", "products");

            // Tạo thư mục nếu chưa tồn tại (không lỗi nếu đã có)
            Directory.CreateDirectory(uploadsRoot);

            // Đặt tên file mới để tránh trùng tên và tránh ký tự lạ trong tên file người dùng
            // Guid.NewGuid():N => tạo chuỗi GUID dạng không dấu gạch (32 ký tự)
            // Giữ lại đuôi ext để browser nhận đúng loại file
            var fileName = $"{Guid.NewGuid():N}{ext}";

            // Ghép thành đường dẫn đầy đủ để ghi file ra đĩa
            var fullPath = Path.Combine(uploadsRoot, fileName);

            // Ghi file vào ổ đĩa theo dạng stream (không tải toàn bộ vào RAM)
            // await using để tự đóng stream sau khi copy xong
            await using (var stream = System.IO.File.Create(fullPath))
            {
                // Copy nội dung file upload vào stream lưu trên server
                await file.CopyToAsync(stream);
            }

            // Trả ra URL công khai để FE có thể hiển thị ảnh
            // Ví dụ: https://localhost:7217/uploads/products/<fileName>
            var url = $"{Request.Scheme}://{Request.Host}/uploads/products/{fileName}";

            // Trả JSON dạng { url: "..." } để FE lấy url và gán vào ImageUrl của product
            return Ok(new { url });
        }
    }
}
