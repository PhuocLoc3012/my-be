using Application.Dtos.AuthDto;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto is null)
            {
                return BadRequest();
            }
            var rs = await _authService.RegisterAsync(userRegistrationDto);
            return Ok(rs);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenDto userAuthen)
        {
            if (userAuthen is null)
            {
                return Unauthorized();
            }
            var rs =  await _authService.AuthenticateAsync(userAuthen);
            return Ok(rs);  
        }

        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string email, [FromQuery] string token)
        {
            await _authService.EmailConfirmationAsync(email, token);
            return Ok();
        }

    }
}
