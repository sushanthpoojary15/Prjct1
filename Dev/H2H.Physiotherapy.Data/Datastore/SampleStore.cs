using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Datastore
{
    public class SampleStore : ISampleStore
    {
        private readonly IDatabaseManager _databaseManager;

        public SampleStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
        //public async Task<List<UserModel>> GetAllUser()
        //{
        //    string commandText = "GetAllUsers";
        //    //IDbDataParameter[] parameters = new IDbDataParameter[]
        //    // {
        //    //    _databaseManager.CreateParameter("@IsNSHR", true, SqlDbType.Bit),
        //    // };
        //    var userInfoList = await _databaseManager.GetAllColumns<UserModel>(commandText);
        //    return userInfoList.ToList();
        //}
        public async Task<List<UserModel>> GetAllUser()
        {
            string commandText = "GetUserByValues";
            IDbDataParameter[] parameters = new IDbDataParameter[]
             {
                _databaseManager.CreateParameter("@Active", true, SqlDbType.Bit),
             };
            var userInfoList = await _databaseManager.GetAllColumns<UserModel>(commandText, parameters);
            return userInfoList.ToList();
        }
    }
}
