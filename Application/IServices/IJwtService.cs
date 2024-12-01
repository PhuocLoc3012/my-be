using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IJwtService
    {
        public string CreateToken (ApplicationUser user);
    }
}
