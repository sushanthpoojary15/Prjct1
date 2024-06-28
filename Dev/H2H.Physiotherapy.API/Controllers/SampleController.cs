using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.SampleModel;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _sampleService;
        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }


        [HttpGet]
        public async Task<IActionResult> GetStudentList()
        {
            ApiResponse response = await _sampleService.GetAllUser();
            response.Complete();
            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> AddFile(FileAdd fileAdd)
        {
            ApiResponse response=await _sampleService.UploadFile(fileAdd);
            response.Complete();
            return Ok(response);
        }
    }
}
