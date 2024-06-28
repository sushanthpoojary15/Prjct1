using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DatabaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface ILoginStore
    {
        public  Task<UserBaseData> GetUsers(string userName);
        public Task UpdatePassword(string UserName, string newPasswordHash);

        public Task InsertOtpReferencNumber(UserDbModel userDbModel, int otp, int otpexpiry, int refernceNumber);

        public Task ValidateOtp(OtpDTO otpDto, int userId);

        public Task<string> CreateRefreshToken(UserModel usr);
        public Task UpdateLastLogin(string userId);

        public Task<RefreshToken> ValidateAndExpireRefreshToken(RefreshTokenPost refreshToken);
    }
}
