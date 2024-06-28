using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IServices
{
    public interface ILoginService
    {
        public Task<ApiResponse> ForgetPassword(ForgetPassDto dto);
        public Task<ApiResponse> ResetPassword(ResetPassDto dto);
        public Task<ApiResponse> Login(UserCredential userCredential);

        public Task<ApiResponse> GenerateOtp(string username);

        public Task<ApiResponse> ValidateOtp(OtpDTO otpDto);

        public Task<object> GenerateAccessTokenFromRefreshToken(RefreshTokenPost refreshToken);
    }
}
