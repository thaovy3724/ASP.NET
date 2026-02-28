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
        //[HttpPost("upload-image")]
        //public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        //{
        //    if (file is null || file.Length == 0)
        //        return BadRequest("File is empty.");

        //    var uploadsRoot = Path.Combine(env.WebRootPath ?? "wwwroot", "uploads", "products");
        //    Directory.CreateDirectory(uploadsRoot);

        //    var ext = Path.GetExtension(file.FileName);
        //    var fileName = $"{Guid.NewGuid():N}{ext}";
        //    var fullPath = Path.Combine(uploadsRoot, fileName);

        //    await using (var stream = System.IO.File.Create(fullPath))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    var url = $"{Request.Scheme}://{Request.Host}/uploads/products/{fileName}";
        //    return Ok(url);
        //}
    }
}
