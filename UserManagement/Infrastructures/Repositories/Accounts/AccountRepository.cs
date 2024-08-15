using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Accounts
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepo
    {
        private readonly UserManagementDbContext _context;
        public AccountRepository(UserManagementDbContext context, ICurrentTime timeService,IClaimsService claimsService): base(context, timeService, claimsService)
        {
            _context = context;
        }
        public async Task<Account> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(record => record.Email == email
                                                                && record.Password == password);
            if (user is null)
            {
                throw new Exception("Email & password is not correct");
            }

            return user;
        }
    }
}
