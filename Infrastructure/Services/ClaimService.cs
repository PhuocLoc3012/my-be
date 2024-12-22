using Application.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ClaimService : IClaimService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid CurrentUserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }

        }

        public List<string>? CurrentUserRoles 
        {
            get
            {
                var roles = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role);
                return roles?.Select(x => x.Value)?.ToList() ?? new List<string>();
            }

        }


    }
}
