using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IServices
{
    public interface IUserService
    {
        public Task<ApiResponse> GetAllUsers();
        public Task<ApiResponse> GetUserById(string UserId);

        public Task<ApiResponse> InActivateUser(string UserId);

        public Task<ApiResponse> UpsertUser(UserAddDTO userBaseData);
    }
}
