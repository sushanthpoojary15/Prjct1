using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnquiryFormController : ControllerBase
    {
        private readonly IEnquiryService _enquiryService;

        public EnquiryFormController(IEnquiryService enquiryService)
        {
            _enquiryService = enquiryService;
        }
        

        [HttpPost]
        [Route("UpsertEnquiry")]
        public async Task<IActionResult> UpsertEnquiry([FromBody] EnquiryDTO enquiry)
        {
            ApiResponse response = await _enquiryService.UpsertEnquiry(enquiry);
            response.Complete();
            return Ok(response);
        }


    }
}
