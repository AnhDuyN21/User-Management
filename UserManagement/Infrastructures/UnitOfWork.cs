using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserManagementDbContext context;

        private readonly IAccountRepo accountRepository;
        private readonly IRoleRepository roleRepository;
        public UnitOfWork(UserManagementDbContext _context, IAccountRepo _accountRepo, IRoleRepository roleRepository)
        {
            context = _context;
            accountRepository = _accountRepo;
            this.roleRepository = roleRepository;

        }


        public IAccountRepo accountRepo => accountRepository;
        public IRoleRepository roleRepo => roleRepository;

        

        public async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
