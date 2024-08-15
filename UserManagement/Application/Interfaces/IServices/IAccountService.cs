using Application.Commons;
using Application.ViewModels.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync();
        Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id);
        Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createdAccountDTO);
        Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, UpdateAccountDTO userDTO);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
    }
}
