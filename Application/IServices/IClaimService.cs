using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IClaimService
    {
        public Guid CurrentUserId { get;}
        public List<string>? CurrentUserRoles { get;}
    }
}
