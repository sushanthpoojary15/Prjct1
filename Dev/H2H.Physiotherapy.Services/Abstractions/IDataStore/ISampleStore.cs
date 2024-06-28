using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface ISampleStore
    {

        public Task<List<UserModel>> GetAllUser();
    }
}
