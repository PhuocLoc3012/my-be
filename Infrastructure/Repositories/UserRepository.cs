using Domain.Entities;
using Domain.IRepository;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
      
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

    }
}
