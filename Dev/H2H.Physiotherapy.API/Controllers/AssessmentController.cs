using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet]
        [Route("GetPainAssessment")]
        public async Task<IActionResult> GetPainAssessment()
        {
            ApiResponse response = await _assessmentService.GetPainAssessment();    
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetPainAssessmentById/{painAssessmentId}")]
        public async Task<IActionResult> GetPainAssessmentById([FromRoute] string painAssessmentId)
        {
            ApiResponse response = await _assessmentService.GetPainAssessmentById(painAssessmentId);
            response.Complete();
            return Ok(response);
        }

        
        [HttpPost]
        [Route("UpsertPainAssessment")]
        public async Task<IActionResult> UpsertPainAssessment([FromBody] PainAssessmentBase painAssessmentDto)
        {
            ApiResponse response = await _assessmentService.UpsertPainAssessment(painAssessmentDto);
            response.Complete();
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeletePainAssessment/{painAssessmentId}")]
        public async Task<IActionResult> DeletePainAssessment([FromRoute] string painAssessmentId )
        {
            ApiResponse response = await _assessmentService.DeletePainAssessment(painAssessmentId);
            response.Complete();
            return Ok(response);
        }


    }
}
