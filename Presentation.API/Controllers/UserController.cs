using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IClaimService _claimService;
        private IUserService _userService;
        public UserController(IClaimService claimService, IUserService userService)
        {
            _claimService = claimService;
            _userService = userService;
        }
        [Authorize]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUserId = _claimService.CurrentUserId;
            var rs = await _userService.GetUserById(currentUserId);
            return Ok(rs);
        }
    }
}
