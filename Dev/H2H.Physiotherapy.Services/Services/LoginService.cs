using AutoMapper;
using Azure;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Utilities;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using H2H.Physiotherapy.Services.Configs;
using H2H.Physiotherapy.Services.Exceptions;
using H2H.Physiotherapy.Services.Features.OtherServices;
using H2H.Physiotherapy.Services.Request;
using Microsoft.Extensions.Options;
using PasswordGenerator;
using H2H.Physiotherapy.Services.Configs;
using H2H.Physiotherapy.Services.Request;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2H.Physiotherapy.Common.Models.DatabaseModels;

namespace H2H.Physiotherapy.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginStore _loginStore;
        private readonly IMapper _mapper;
        private readonly IRequestContext _requestContext;
        private readonly IAccountStore _accountStore;
        private readonly ICommonServices _commonServices;
        private readonly int otpExpiryTime;
        private readonly IEmailService _emailService;
         private readonly TokenSettings _tokenSettings;

        public LoginService(ILoginStore loginStore, IMapper mapper, IRequestContext requestContext,IOptions<PhysioTheraphyConfigurations> configuration, ICommonServices commonServices, IEmailService emailService, IAccountStore accountStore)
        {
            this._tokenSettings = configuration.Value.TokenSettings;
            _loginStore = loginStore;
            _requestContext = requestContext;
            _mapper = mapper;
            _accountStore = accountStore;
            _commonServices = commonServices;
            otpExpiryTime = configuration.Value.otpSettings.OtpExpiry;
            _emailService = emailService;
        }

        public string GeneratePassword()
        {
            var pwd = new Password(15).IncludeLowercase().IncludeUppercase().IncludeNumeric().IncludeSpecial("*@_");
            var password = pwd.Next();
            return password;
        }

        public async Task<ApiResponse> ForgetPassword(ForgetPassDto dto)
        {
            ApiResponse Response = new ApiResponse();
            var userDetails = await _loginStore.GetUsers(dto.UserName);
            if (userDetails == null)
            {
                Response.Response = "Invalid User";
        
            }
            if (userDetails.DateOfBirth == dto.DateOfBirth && userDetails.EmailId == dto.EmailId)
            {
                string tempPassword = GeneratePassword();
                Console.WriteLine(tempPassword);
                string tempPasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(tempPassword);
                await _loginStore.UpdatePassword(dto.UserName, tempPasswordHash);
                Response.Response = "Email Successfully Sent";
            }
            else
            {
                Response.Response = "User Not Found";
            }
            Response.ResponseStatus = "Success";
            Response.Message = "Forget Password Successful";
            return Response;
        }

        public async Task<ApiResponse> ResetPassword(ResetPassDto dto)
        {
            ApiResponse Response = new ApiResponse();
            //string Username= _requestContext.Username;
            string userName = "user1";
            var user = await _loginStore.GetUsers(userName);
            if (user == null)
            {
                Response.Response = "Invalid User";
                Response.ResponseStatus = "success";
                Response.Message = "Forget Password Successful";
            }

            if (BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, user.PasswordHash))
            {
                string newPasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.NewPassword);
                await _loginStore.UpdatePassword(userName, newPasswordHash);
                Response.Response = "Password Resetted";
                Response.ResponseStatus = "success";
                Response.Message = "Reset Password Successful";
                return Response;
            }
            else
            {
                Response.Response = "Old User Password does not match";
                Response.ResponseStatus = "success";
                Response.Message = "Reset Password Successful";
                return Response;
            }

            
        }

        public async Task<ApiResponse> Login(UserCredential userCredential)
        {
            ApiResponse response = new ApiResponse();
            var user = await _loginStore.GetUsers(userCredential.UserName);

            if (user == null)
            {
                response.ResponseStatus = "Error";
                response.Message = "Invalid username or password";
                return response;
            }

            if (!BCrypt.Net.BCrypt.EnhancedVerify(userCredential.Password, user.PasswordHash))
            {
                response.ResponseStatus = "Error";
                response.Message = "Invalid username or password";
                return response;
            }


            UserModel userModel = _mapper.Map<UserModel>(user);
            
            var token = TokenHelper.GenerateToken(userModel, _tokenSettings.TokenSigningKey, _tokenSettings.TokenExpiryTime);

            response.Response = new
            {
                access_token = token,
                token_type = "bearer",
                expires_in = new TimeSpan(0, _tokenSettings.TokenExpiryTime, 0).TotalSeconds.ToString(),
                user_id=userModel.UserId
            };

            response.ResponseStatus = "success";
            response.Message = "Login successful";


            return response;


        }
            
        

        public async Task<ApiResponse> GenerateOtp(string username)
        {
            ApiResponse response = new ApiResponse();
            var user = await _accountStore.GetUserDataFromUserName(username);
            if (user == null)
            {
                throw new DataNotFoundException("User not found");
            }
            int refernceNumber = await _commonServices.Generate8DigitReferenceNumber();
            int otp = await _commonServices.Generate6DigitOtp();
            await _loginStore.InsertOtpReferencNumber(user, otp, otpExpiryTime, refernceNumber);
            OtpDTO otpDTO = new OtpDTO
            {
                UserName = username,
                OtpValidity = otpExpiryTime,
                ReferenceNumber = refernceNumber
            };
            var mailbody = string.Format("Hi {0} ,<br/><br/>Your OTP has been successfully generated.<br/><br/>Reference Number: {1}<br/><span style='padding-left:15px;'>OTP: {2}</span><br/><br/>For any queries or issues, please contact the administrator at admin@Health2Home.com.<br/><br/>Thank you<br/>",
                user.UserName,
                refernceNumber,
                otp
                );
            _emailService.SendAutomatedMail("sushanth@sysfore.com", "OTP Login", mailbody);
            response.Response = otpDTO;
            response.Message = "Otp generated Successfully";
            response.ResponseStatus = "success";
            return response;
        }

        public async Task<ApiResponse> ValidateOtp(OtpDTO otpDto)
        {
            ApiResponse response = new ApiResponse();
            var user = await _accountStore.GetUserDataFromUserName(otpDto.UserName);
            if (user == null)
            {
                throw new DataNotFoundException("User not found");
            }
            await _loginStore.ValidateOtp(otpDto, user.Id);
            UserModel userModel = _mapper.Map<UserModel>(user);
            var token = TokenHelper.GenerateToken(userModel, _tokenSettings.TokenSigningKey, _tokenSettings.TokenExpiryTime);
            var refreshtoken = await _loginStore.CreateRefreshToken(userModel);
            await _loginStore.UpdateLastLogin(userModel.UserId);
            //token generation to be implemented
            response.Response = new { access_token = token, token_type = "bearer", expires_in = new TimeSpan(0, _tokenSettings.TokenExpiryTime, 0).TotalSeconds.ToString(), refresh_token = refreshtoken,user_id=userModel.UserId}; 
            response.Message = "Login Successful";
            response.ResponseStatus = "success";
            return response;
        }

        public async Task<object> GenerateAccessTokenFromRefreshToken(RefreshTokenPost refreshToken)
        {
        
                var newrefreshtoken = await _loginStore.ValidateAndExpireRefreshToken(refreshToken);
                if (refreshToken == null)
                {
                    throw new DataNotFoundException("Refresh token doesn't exist");
                }
                if (newrefreshtoken.Refreshtoken == null)
                {
                    throw new DataNotFoundException("Cannot generate refresh token");
                }
                var userModel = new UserModel() { Code = newrefreshtoken.RoleCode, UserId = newrefreshtoken.UserId, UserName = newrefreshtoken.UserName };
                var token = TokenHelper.GenerateToken(userModel, _tokenSettings.TokenSigningKey, _tokenSettings.TokenExpiryTime);
                
                return new { access_token = token, token_type = "bearer", expires_in = new TimeSpan(0, _tokenSettings.TokenExpiryTime, 0).TotalSeconds.ToString(), refresh_token = newrefreshtoken.Refreshtoken,user_id=userModel.UserId };

        }

    }
}
