using Application.Commons;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Ultils;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Domain.Entities;
using System.Data.Common;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync()
        {
            var response = new ServiceResponse<IEnumerable<AccountDTO>>();
            try
            {
                var users = await _unitOfWork.accountRepo.GetAllAsync();

                var userDTOs = new List<AccountDTO>();

                foreach (var user in users)
                {
                    var account = _mapper.Map<AccountDTO>(user);
                    userDTOs.Add(account);
                }

                if (userDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = userDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Account";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return response;
        }

        public async Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var response = new ServiceResponse<AccountDTO>();

            var exist = await _unitOfWork.accountRepo.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }
            else
            {
                response.Success = true;
                response.Message = "Account found";
                response.Data = _mapper.Map<AccountDTO>(exist);
            }

            return response;
        }


        public async Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreateAccountDTO createdAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();


            try
            {
                //Mapping
                var account = _mapper.Map<Account>(createdAccountDTO);
                //HashPassword
                account.Password = HashPassword.HashWithSHA256(createdAccountDTO.Password);
                //GenerateToken
                account.ConfirmationToken = Guid.NewGuid().ToString();

                account.IsDeleted = false;

                await _unitOfWork.accountRepo.AddAsync(account);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var accountDTO = _mapper.Map<AccountDTO>(account);
                    response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                    response.Success = true;
                    response.Message = "User created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, UpdateAccountDTO userDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            try
            {
                var existingUser = await _unitOfWork.accountRepo.GetByIdAsync(id);

                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "Account not found.";
                    return response;
                }

                if (existingUser.IsDeleted == true)
                {
                    response.Success = false;
                    response.Message = "Account is deleted in system";
                    return response;
                }


                // Map accountDT0 => existingUser
                var updated = _mapper.Map(userDTO, existingUser);

                _unitOfWork.accountRepo.Update(existingUser);

                var updatedUserDto = _mapper.Map<AccountDTO>(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    response.Data = updatedUserDto;
                    response.Success = true;
                    response.Message = "Account updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.accountRepo.GetByIdAsync(id);
            if (exist == null || exist.IsDeleted == true)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                _unitOfWork.accountRepo.SoftRemove(exist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

    }
}
