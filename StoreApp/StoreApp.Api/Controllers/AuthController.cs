using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.AuthUseCase.Command.Login;
using StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
