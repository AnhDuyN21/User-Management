using Application.Commons;
using Application.ViewModels.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterDTO registerAccountDTO);
        public Task<ServiceResponse<string>> LoginAsync(LoginDTO accountDTO);
    }
}
