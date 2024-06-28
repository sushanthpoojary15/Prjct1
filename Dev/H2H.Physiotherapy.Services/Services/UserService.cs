using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using H2H.Physiotherapy.Services.Features.OtherServices;
using H2H.Physiotherapy.Services.Request;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Services
{
    [Authorize]
    public class UserService:IUserService
    {
        private readonly IUserStore _userStore;
        private readonly IEmailService _emailService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IRequestContext _requestContext;
        public UserService(IUserStore userStore,IEmailService emailService, IFileUploadService fileUploadService,IRequestContext requestContext)
        {
            _userStore = userStore;
            _emailService = emailService;
            _fileUploadService = fileUploadService;
            _requestContext = requestContext;
        }

        public async Task<ApiResponse> GetAllUsers()
        {
            ApiResponse Response = new ApiResponse();
            var userList = await _userStore.GetAllUsers();
            var updatedUser = userList.Select(user =>
            {
                var sasToken = _fileUploadService.GenerateSharedAcessSignatureForBlobContainer().Result;
                user.ImagePath = user.ImagePath!="" ? user.ImagePath+sasToken:user.ImagePath;
                return user;
            }).ToList();
            Response.Response = updatedUser;
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched All Users";
            return Response;
        }

        public async Task<ApiResponse> GetUserById(string UserId)
        {
            ApiResponse Response = new ApiResponse();
            var user = await _userStore.GetUserById(UserId);
            var sasToken = _fileUploadService.GenerateSharedAcessSignatureForBlobContainer().Result;
            user.ImagePath =user.ImagePath+""+sasToken;
            Response.Response = user;
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched User";

            return Response;
        }

        public async Task<ApiResponse> InActivateUser(string UserId)
        {
            ApiResponse Response = new ApiResponse();
            string currentUser = _requestContext.UserId;
            await _userStore.InActivateUser(UserId,currentUser);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully InActivatedUser";
            return Response;

        }

        public async Task<ApiResponse> UpsertUser(UserAddDTO userData)
        {

            ApiResponse Response = new ApiResponse();
            string _currentUser = _requestContext.UserId;
            var genPassword = "TestPassword";
            if (userData.UserId==null)
            {
             
                userData.PasswordHash =BCrypt.Net.BCrypt.EnhancedHashPassword(genPassword);
            }
            if(userData.ImageData!=null && userData.ImageData!="")
            {
                string FileExtension = Path.GetExtension(userData.ImagePath.ToString()).ToLower();
                byte[] data1 = Convert.FromBase64String(userData.ImageData);
                MemoryStream fMs = new MemoryStream(data1);

                BlobFileData fDatablob = new BlobFileData();
                fDatablob.File = fMs;
                fDatablob.blobName = string.Format("User/{0}{1}", userData.UserName, FileExtension);
                fDatablob.container = "physiotheraphy";
                fDatablob.ContentType = string.Format("image/{0}", FileExtension.Trim('.')); //"image/png";
                await _fileUploadService.UploadImageToBlobInBackgroundThread(fDatablob);
                userData.ImagePath = fDatablob.blobName;
            }
            await _userStore.UpsertUser(userData, _currentUser);
            if(userData.UserId==null)
            {
                var mailbody = string.Format("Hi {0},<br/><br/>Click {3} and login with the following userInfo<br/><br/><span style='padding-left:15px;'>Username - <b>{1}</b></span> <br/><span style='padding-left:15px;'>Password - <b>{2}</b></span><br/><br/>For any queries or issues logging in please contact the administrator admin@Health2Home.com<br/><br/>Thank you<br/>",
                                               userData.FirstName,
                                               userData.UserName,
                                               genPassword,
                                               "www.h2h.com");
                //await _emailService.SendAutomatedMail("sysfore.com", "Password Generated", mailbody);
            }
            Response.ResponseStatus = "success";
            Response.Message =userData.UserId=="" ? "Successfully Added User": "Successfully Updated User";
            return Response;
        }
    }
}
