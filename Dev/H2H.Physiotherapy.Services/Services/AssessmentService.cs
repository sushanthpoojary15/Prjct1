using AutoMapper;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentStore _assessmentStore;
        private readonly IMapper _mapper;
        private readonly IRequestContext _requestContext;

        public AssessmentService(IAssessmentStore assessmentStore, IMapper mapper , IRequestContext requestContext)
        {
            _assessmentStore = assessmentStore; 
            _mapper = mapper;
            _requestContext = requestContext;
        }

        public async Task<ApiResponse> GetPainAssessment()
        {
            ApiResponse Response = new ApiResponse();
            Response.Response = await _assessmentStore.GetPainAssessment();
            //Response.Response =  _mapper.Map<List<EquipmentDto>>(painAssessments);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched Pain";

            return Response;
        }

        public async Task<ApiResponse> GetPainAssessmentById(string painAssessmentId)
        {
            ApiResponse Response = new ApiResponse();

            var painAssessmentsDetails = await _assessmentStore.GetPainAssessmentById(painAssessmentId);
            Response.Response = _mapper.Map<List<PainAssessmentDto>>(painAssessmentsDetails);
            Response.ResponseStatus = "success";
            Response.Message = "PainAssessment retrieved successfully";

            return Response;
        }

        public async Task<ApiResponse> UpsertPainAssessment(PainAssessmentBase painAssessment)
        {
            ApiResponse response = new ApiResponse();
            string updatedBy = _requestContext.UserId;
            var isUpdate = !string.IsNullOrEmpty(painAssessment.PainAssessmentId);

            await _assessmentStore.UpsertPainAssessment(painAssessment, updatedBy);

            response.ResponseStatus = "success";
            response.Message = "Saved Successfully";

            return response;
        }

        public async Task<ApiResponse> DeletePainAssessment(string painAssessmentId)
        {
            ApiResponse Response = new ApiResponse();
            await _assessmentStore.DeletePainAssessment(painAssessmentId);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully deleted Assessment";

            return Response;
        }
    }
}
