using Application.Dtos.AuthDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IJwtService
    {
        public TokenDto CreateToken(ApplicationUser user, IList<string> roles, bool populateExp);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
