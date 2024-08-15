using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IAccountRepo : IGenericRepository<Account>
    {
        Task<Account> GetUserByEmailAndPassword(string email, string password);
    }
}
