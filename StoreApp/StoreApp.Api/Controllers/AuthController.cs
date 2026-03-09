using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Application.UseCases.AuthUseCase.Command.Login;
using StoreApp.Application.UseCases.AuthUseCase.Command.RefreshToken;
using StoreApp.Application.UseCases.AuthUseCase.Command.Register;
using StoreApp.Application.UseCases.AuthUseCase.Command.ResendOtp;
using StoreApp.Application.UseCases.AuthUseCase.Command.VerifyOtp;

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

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("verify-otp")]
        public async Task<ActionResult> VerifyOtp([FromBody] VerifyOtpCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }

        [HttpPost("resend-otp")]
        public async Task<ActionResult> ResendOtp([FromBody] ResendOtpCommand cmd)
        {
            var result = await mediator.Send(cmd);
            return Ok(result);
        }
    }
}
