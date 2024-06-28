using AutoMapper;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.SampleModel;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using H2H.Physiotherapy.Services.Features.OtherServices;
using H2H.Physiotherapy.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace H2H.Physiotherapy.Services.Services
{
    public class SampleService : ISampleService
    {
        private readonly ILoggerService _loggerservice;
        private readonly ISampleStore _sampleStore;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IFileUploadService _imageUploadService;
        public SampleService(ILoggerService loggerservice, ISampleStore sampleStore,IMapper mapper,
            IEmailService emailService, IFileUploadService imageUploadService)
        {
            _loggerservice = loggerservice;
            _sampleStore = sampleStore;
            _mapper = mapper;
            _emailService = emailService;
            _imageUploadService = imageUploadService;
        }
        public async Task<ApiResponse> GetAllUser()
        {
            ApiResponse Response = new ApiResponse();

                var a = _sampleStore.GetAllUser();
                Response.Response = await _sampleStore.GetAllUser();
                string userInboxMessage = string.Format("Dear ({0}) successfully fetched the details", "Jerin");
                 await _emailService.SendAutomatedMailwithCustomCC("jerin.jomy@sysfore.com", "Testting functonality", userInboxMessage, true, "abhilash.b@sysfore.com");
         
                Response.ResponseStatus = "success";
                Response.Message = "Success";

            var result = _mapper.Map<UserData>(a);
            return Response;
        }

        public async Task<ApiResponse> UploadFile([FromQuery]FileAdd fileAdd)
        {
            ApiResponse Response = new ApiResponse();
            if(fileAdd.FileName!=null)
            {
                string userName = "jerin";
                string fileExtension = Path.GetExtension(fileAdd.FileName.ToString()).ToLower();
            
                string blobName = $"User/{userName}{fileExtension}";
     
                byte[] data1 = Convert.FromBase64String(fileAdd.ImageData);
                MemoryStream fMs = new MemoryStream(data1);

                BlobFileData fDatablob = new BlobFileData();
                fDatablob.File = fMs;
                fDatablob.blobName = string.Format("User/{0}{1}", userName, fileExtension);
                fDatablob.container = "h2h";
                fDatablob.ContentType = string.Format("image/{0}", fileExtension.Trim('.')); //"image/png";
               var imagePath= await _imageUploadService.UploadImageToBlobInBackgroundThread(fDatablob);
               var sasToken = _imageUploadService.GenerateSharedAcessSignatureForBlobContainer();
               Response.Response=  new Uri($"{imagePath}{sasToken.Result}");
                //userInfo.ImagePath = fDatablob.blobName;
            }
            //var a = _sampleStore.GetAllUser();
            //Response.Response = await _sampleStore.GetAllUser();
            //string userInboxMessage = string.Format("Dear ({0}) successfully fetched the details", "Jerin");
            //await _emailService.SendAutomatedMailwithCustomCC("jerin.jomy@sysfore.com", "Testting functonality", userInboxMessage, true, "abhilash.b@sysfore.com");

            Response.ResponseStatus = "success";
            Response.Message = "Success";

            //var result = _mapper.Map<UserData>(a);
            return Response;
        }
    }
}
