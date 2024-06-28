using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.DatabaseModels;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Datastore
{
    public class AccountStore : IAccountStore
    {
        private readonly IDatabaseManager _databaseManager;

        public AccountStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public async Task<UserDbModel> GetUserDataFromUserName(string userName)
        {
            string commandText = "usp_GetUserDetails";

            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@userName", userName, SqlDbType.NVarChar)
            };

            var userDbmodel = await _databaseManager.GetAllColumns<UserDbModel>(commandText, parameters);
            if (!userDbmodel.Any())
            {
                return null;
            }

            //UserData userData = GetUserData(userDbmodel);
            return userDbmodel.FirstOrDefault();
        }

       




    }
}
