using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApp.Application.UseCases.UserUseCase.Command.BulkDelete;
using StoreApp.Application.UseCases.UserUseCase.Command.Create;
using StoreApp.Application.UseCases.UserUseCase.Command.Delete;
using StoreApp.Application.UseCases.UserUseCase.Command.Update;
using StoreApp.Application.UseCases.UserUseCase.Query.GetList;
using StoreApp.Application.UseCases.UserUseCase.Query.GetOne;
using System.Security.Claims;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [Authorize(Roles = "Admin, Staff")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cmd = new GetUserQuery(Id: id);
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Staff")]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListUserQuery cmd)
        {
            var result = await mediator.Send(cmd);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return Ok(result.Items);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand cmd)
        {
            cmd = cmd with { Id = id };
            await mediator.Send(cmd);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(currentUserIdStr, out var currentUserId) && currentUserId == id)
            {
                return BadRequest(new
                {
                    message = "Bạn không thể tự xóa chính mình khi đang đăng nhập."
                });
            }

            var cmd = new DeleteUserCommand(Id: id);
            await mediator.Send(cmd);
            return NoContent();
        }


        // endpoint xóa nhiều user cùng lúc, chỉ admin mới có quyền thực hiện
        [Authorize(Roles = "Admin")]
        [HttpPost("bulk-delete")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteUserCommand cmd)
        {
            var currentUserIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(currentUserIdStr, out var currentUserId))
            {
                return Unauthorized();
            }

            cmd = cmd with { CurrentUserId = currentUserId };

            await mediator.Send(cmd);
            return NoContent();
        }
    }
}