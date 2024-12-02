
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "OnlyAdminUsers")]
        public IActionResult TestAction() => Ok("TEST MESSAGE");

        [HttpGet("ping")]  // Thêm endpoint test
        public IActionResult Ping() => Ok("pong");
    }
}
