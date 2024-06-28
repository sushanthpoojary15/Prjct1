﻿using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface IAccountStore
    {
        public Task<UserDbModel> GetUserDataFromUserName(string userName);


    }
}
