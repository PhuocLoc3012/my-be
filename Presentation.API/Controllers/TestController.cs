
using Application.Dtos.AuthDto;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IEmailSender _emailSender;
        public TestController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpGet]
        [Authorize(Policy = "OnlyAdminUsers")]
        public IActionResult TestAction() => Ok("TEST MESSAGE");

        [HttpGet("ping")]  // Thêm endpoint test
        public IActionResult Ping() => Ok("PONG");

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest == null || string.IsNullOrEmpty(emailRequest.ToEmail))
            {
                return BadRequest("Email address is required.");
            }
            await _emailSender.SendEmailAsync(emailRequest.ToEmail, emailRequest.Subject, emailRequest.Body);
            return Ok("Email sent successfully!");
        }
    }
}
