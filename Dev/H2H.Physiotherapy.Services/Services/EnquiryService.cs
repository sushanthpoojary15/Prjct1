using AutoMapper;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using H2H.Physiotherapy.Services.Request;

namespace H2H.Physiotherapy.Services.Services
{
    public class EnquiryService : IEnquiryService
    {
        private readonly IEnquiryStore _enquiryStore;
        private readonly IMapper _mapper;
        private readonly IRequestContext _requestContext;

        public EnquiryService(IEnquiryStore enquiryStore, IMapper mapper, IRequestContext requestContext)
        {
            _enquiryStore = enquiryStore;
            _mapper = mapper;
            _requestContext = requestContext;
        }

        public async Task<ApiResponse> UpsertEnquiry(EnquiryDTO enquiry)
        {
            ApiResponse Response = new ApiResponse();

            string _currentUser = _requestContext.UserId;
            var isUpdate = !string.IsNullOrEmpty(enquiry.EnquiryId);
            await _enquiryStore.UpsertEnquiry(enquiry, _currentUser);
            Response.ResponseStatus = "success";
            Response.Message = isUpdate ? "Enquiry updated successfully" : "Enquiry added successfully";

            return Response;
        }
    }
}