using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.AuthUseCase.Command.Login;
using StoreApp.Application.UseCases.AuthUseCase.Command.Logout;
using StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken;
using StoreApp.Application.UseCases.AuthUseCase.Command.Register;

namespace StoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return NoContent();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
