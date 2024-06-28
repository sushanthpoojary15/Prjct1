using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2H.Physiotherapy.Common.Models.DTO;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface IUserStore
    {
        public Task<List<UserDTO>> GetAllUsers();
        public Task<UserDTO> GetUserById(string UserId);

        public Task InActivateUser(string UserId,string currentUser);

        public Task UpsertUser(UserAddDTO userBaseData,string currentUser);
    }
}
