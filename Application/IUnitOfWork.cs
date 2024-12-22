using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
  
        Task<int> SaveChangesAsync();
    }
}
