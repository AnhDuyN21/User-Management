using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Ultils;
using Application.Utils;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentTime _currentTime;
        private readonly IConfiguration _configuration;

        //private IValidator<Account> _validator;
        private readonly IMapper _mapper;

        public AuthenticationService(
        IUnitOfWork unitOfWork,
        ICurrentTime currentTime,
        IConfiguration configuration,
        IMapper mapper
)
        {
            _unitOfWork = unitOfWork;
            _currentTime = currentTime;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> LoginAsync(LoginDTO accountDTO)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var hashedPassword = HashPassword.HashWithSHA256(accountDTO.Password);
                var user = await _unitOfWork.accountRepo.GetUserByEmailAndPassword(accountDTO.Email, hashedPassword);

                if (user.IsDeleted == true)
                {
                    response.Success = false;
                    response.Message = "Your account have been deleted!";
                    return response;
                }

                var generate = new GenerateJsonWebTokenString(_unitOfWork);

                var token = generate.GenerateJsonWebToken(user, _configuration, _configuration.GetSection("JWTSection:SecretKey").Value , _currentTime.GetCurrentTime());

                //var token = user.GenerateJsonWebToken(
                //    _configuration,
                //    _configuration.JWTSection.SecretKey,
                //    _currentTime.GetCurrentTime()
                //    );

                response.Success = true;
                response.Message = "Login successfully.";
                response.Data = token;
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

        public async Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterDTO registerAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();
            try
            {
                //var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(registerAccountDTO.Email);
                //if (exist)
                //{
                //    response.Success = false;
                //    response.Message = "Email is existed";
                //    return response;
                //}
                var user = _mapper.Map<Account>(registerAccountDTO);
                user.Password = HashPassword.HashWithSHA256(registerAccountDTO.Password);
                // Tạo token ngẫu nhiên
                user.ConfirmationToken = Guid.NewGuid().ToString();
                user.RoleId = 1; 
                user.IsDeleted = false;
                await _unitOfWork.accountRepo.AddAsync(user);
                var confirmationLink = $"https://localhost:5001/swagger/confirm?token={user.ConfirmationToken}";
                // Gửi email xác nhận
                //var emailSent = await SendEmail.SendConfirmationEmail(user.Email, confirmationLink);
                //if (!emailSent)
                //{
                //    // Xử lý khi gửi email không thành công
                //    response.Success = false;
                //    response.Message = "Error sending confirmation email.";
                //    return response;
                //}
                //else
                //{
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        var userDTO = _mapper.Map<AccountDTO>(user);
                        response.Data = userDTO; // Chuyển đổi sang UserDTO
                        response.Success = true;
                        response.Message = "Register successfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error saving the account.";
                    }
                //}
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
    }
}
