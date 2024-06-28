using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Datastore
{
    public class UserStore : IUserStore
    {
        private readonly IDatabaseManager _databaseManager;


        public UserStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            string commandText = "usp_GetAllUserData";

            var UserDataList = await _databaseManager.GetAllColumns<UserDTO>(commandText);

            return UserDataList.ToList();
        }

        public async Task<UserDTO> GetUserById(string UserId)
        {
            string commandText = "usp_GetUserById";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@UserId",UserId,SqlDbType.NVarChar)
            };
            var UserData = await _databaseManager.GetAllColumns<UserDTO>(commandText,parameters);
            if (UserData.FirstOrDefault() == null)
            {
                throw new DataNotFoundException("User Doesnot Exists");
            }
            return UserData.FirstOrDefault();

        }

        public async Task InActivateUser(string UserId, string currentUser)
        {
            string commandText = "usp_InactivateUser";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@UserId",UserId,SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@UpdatedBy",currentUser,SqlDbType.NVarChar)
            };
            await _databaseManager.Update(commandText,parameters);
        }

        public async Task UpsertUser(UserAddDTO userAddDTO,string currentUser)
        {
            try
            {
                string commandText = "usp_UpsertUser";
                IDbDataParameter[] parameters = new IDbDataParameter[]
                {
                // Example for creating SQL parameters

                    _databaseManager.CreateParameter("@userId", userAddDTO.UserId, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@userName", userAddDTO.UserName, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@firstName", userAddDTO.FirstName, SqlDbType.VarChar),
                    _databaseManager.CreateParameter("@lastName", userAddDTO.LastName, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@employeeId", userAddDTO.EmployeId, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@dateOfBirth", userAddDTO.DateOfBirth, SqlDbType.Date),
                    _databaseManager.CreateParameter("@emailId", userAddDTO.EmailId, SqlDbType.VarChar),
                    _databaseManager.CreateParameter("@contactNo", userAddDTO.ContactNo, SqlDbType.BigInt),
                    _databaseManager.CreateParameter("@country", userAddDTO.CountryId, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@roleId", userAddDTO.RoleId, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@passwordHash", userAddDTO.PasswordHash, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@supervisorId", userAddDTO.SupervisorId, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@workingHours", userAddDTO.WorkingHours, SqlDbType.Int),
                    _databaseManager.CreateParameter("@emiratesId", userAddDTO.EmiratesId, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@emirates", userAddDTO.Emirates, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@passPortNumber", userAddDTO.PassPortNumber, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@passPortExpiry", userAddDTO.PassPortExpiry, SqlDbType.Date),
                    _databaseManager.CreateParameter("@visaNumber", userAddDTO.VisaNumber, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@visaExpiry", userAddDTO.VisaExpiry, SqlDbType.Date),
                    _databaseManager.CreateParameter("@qualification", userAddDTO.Qualification, SqlDbType.Text),
                    _databaseManager.CreateParameter("@bio", userAddDTO.Bio, SqlDbType.Text),
                    _databaseManager.CreateParameter("@imagePath", userAddDTO.ImagePath, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@address", userAddDTO.Address, SqlDbType.Text),
                    _databaseManager.CreateParameter("@identityColor", userAddDTO.IdentityColor, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@updatedBy", currentUser, SqlDbType.NVarChar),
                    _databaseManager.CreateParameter("@changePasswordOnNextLogin", userAddDTO.ChangePasswordOnNextLogin, SqlDbType.Bit)

                };
                await _databaseManager.Update(commandText, parameters);
            }catch(DbException ex) 
            {
                 throw new DataNotFoundException(ex.Message);
            }

        }
    }
}
