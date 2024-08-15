using Application.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepo accountRepo { get; }
        public IRoleRepository roleRepo { get; }

        public Task<int> SaveChangeAsync();
    }
}
