using Azure.Identity;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DatabaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Exceptions;
using H2H.Physiotherapy.Services.Request;
using PasswordGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Datastore
{
    public class LoginStore: ILoginStore
    {
        private readonly IDatabaseManager _databaseManager;


        public LoginStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<UserBaseData> GetUsers(string userName)
        {
            string commandText = "usp_GetUserDetails";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@UserName", userName, SqlDbType.NVarChar)
            };
            var user = await _databaseManager.Select<UserBaseData>(commandText, parameters);
            if(user==null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }
            return user.FirstOrDefault();
        }

        public async Task UpdatePassword(string UserName, string newPasswordHash)
        {
            string commandText = "usp_ResetPassword";
            var updateParameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@Username",UserName, DbType.String),
                _databaseManager.CreateParameter("@NewPasswordHash", newPasswordHash, DbType.String)
            };
            await _databaseManager.Update(commandText, updateParameters);
          
        }


        public async Task InsertOtpReferencNumber(UserDbModel userDbModel, int otp, int otpexpiry, int refernceNumber)
        {
            string commandText = "uspInsertOtp";

            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
            _databaseManager.CreateParameter("@generatedFor", userDbModel.Id, SqlDbType.Int),
            _databaseManager.CreateParameter("@referenceId", refernceNumber, SqlDbType.Int),
             _databaseManager.CreateParameter("@otpvalue", otp, SqlDbType.Int),
            _databaseManager.CreateParameter("@otpexpiry", otpexpiry , SqlDbType.Int)
             };

            await _databaseManager.Insert(commandText, parameters);
        }

        public async Task ValidateOtp(OtpDTO otpDto, int userId)
        {
            try
            {
                string commandText = "usp_ValidateOtp";
                IDbDataParameter[] parameters = new IDbDataParameter[]
                {
            _databaseManager.CreateParameter("@generatedFor", userId, SqlDbType.Int),
            _databaseManager.CreateParameter("@referenceId", otpDto.ReferenceNumber, SqlDbType.Int),
             _databaseManager.CreateParameter("@otpvalue", otpDto.OtpValue, SqlDbType.Int),
                };

                await _databaseManager.Update(commandText, parameters);
            }
            catch (DbException ex)
            {
                throw new InvalidChangesException(ex.Message);
            }

        }

        public async Task UpdateLastLogin(string userId)
        {
            string commandText = "usp_UpdateLastLogin";

            IDbDataParameter[] parameters = new IDbDataParameter[]
           {
                _databaseManager.CreateParameter("@userId", userId, SqlDbType.NVarChar)
           };
            
           await _databaseManager.Update(commandText, parameters);
            
        }

        public async Task<string> CreateRefreshToken(UserModel usr)
        {
            string refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            string commandText = "usp_CreateRefreshToken";
            IDbDataParameter[] parameters = new IDbDataParameter[]  {
                    _databaseManager.CreateParameter("@UserId", usr.UserId,  SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@RefreshToken", refreshToken, SqlDbType.NVarChar),

                };
            await _databaseManager.Insert(commandText, parameters);
            return refreshToken;
        }

        public async Task<RefreshToken> ValidateAndExpireRefreshToken(RefreshTokenPost refreshToken)
        {
            RefreshToken rf = new RefreshToken();
            string commandText = "usp_ValidateAndExpireRefreshToken";
            string newrefreshToken = Guid.NewGuid().ToString().Replace("-", "");
            IDbDataParameter[] parameters = new IDbDataParameter[]  {
                   _databaseManager.CreateParameter("@UserId",refreshToken.UserId,SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@RefreshToken", refreshToken.Refreshtoken,  DbType.String),
                    _databaseManager.CreateParameter("@NewRefreshToken",newrefreshToken,DbType.String)
                };
            var newrefreshTokens = await _databaseManager.GetAllColumns<RefreshToken>(commandText, parameters);
            RefreshToken refreshTokenResult = newrefreshTokens.FirstOrDefault();

            return refreshTokenResult;

        }

    }
}
