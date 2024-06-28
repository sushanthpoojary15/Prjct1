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
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetCategories()
        {
            ApiResponse response = await _masterService.GetAllCategories();
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllPhysiotherapyType")]

        public async Task<IActionResult> GetAllPhsiotheraphy()
        {
            ApiResponse response = await _masterService.GetAllPhysiotheraphy();
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetRolesList")]
        public async Task<IActionResult> GetRolesList()
        {
            ApiResponse response = await _masterService.GetAllRoles();
            response.Complete();
            return Ok(response);
        }

        [HttpGet("GetAllEmirates")]
        public async Task<IActionResult> GetAllEmirates()
        {
            ApiResponse response = await _masterService.GetAllEmirates();
            response.Complete();
            return Ok(response);
        }

        [HttpGet("GetAllDevices")]
        public async Task<IActionResult> GetAllDevices()
        {
            var response = await _masterService.GetAllDevices();
            response.Complete();
            return Ok(response);
        }

        [HttpGet(" GetDeviceByDeviceId/{deviceId}")]
        public async Task<IActionResult> GetDeviceByDeviceId([FromRoute] string deviceId)
        {
            var response = await _masterService.GetDeviceByDeviceId(deviceId);
            response.Complete();
            return Ok(response);
        }

        [HttpDelete("DeleteDevice/{deviceId}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] string deviceId)
        {
            var response = await _masterService.DeleteDevice(deviceId);
            response.Complete();
            return Ok(response);
        }

        [HttpPost("upsertDevice")]
        public async Task<IActionResult> UpsertDevice([FromBody] DevicesDto deviceDto)
        {
            var response = await _masterService.UpsertDevice(deviceDto);
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllEnquiryType")]
        public async Task<IActionResult> GetEnquiryTypeList()
        {
            ApiResponse response = await _masterService.GetAllEnquiryType();
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllServicesList")]
        public async Task<IActionResult> GetAllServicesList()
        {
            ApiResponse response = await _masterService.GetAllServices();
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetServiceById")]
        public async Task<IActionResult> GetServiceById(string id)
        {
            ApiResponse response = await _masterService.GetServiceById(id);
            response.Complete();
            return Ok(response);
        }

        [HttpPost]
        [Route("AddService")]
        public async Task<IActionResult> AddService(ServiceDto service)
        {
            ApiResponse response = await _masterService.AddService(service);
            response.Complete();
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateService")]
        public async Task<IActionResult> UpdateService(string id, [FromBody] ServiceUpdateDto service)
        {
            ApiResponse response = await _masterService.UpdateService(id, service);
            response.Complete();
            return Ok(response);
        }

        [HttpDelete]
        [Route("RemoveService")]
        public async Task<IActionResult> DeleteService(string id)
        {
            ApiResponse response = await _masterService.DeleteService(id);
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllEquipment")]
        public async Task<IActionResult> GetAllEquipments()
        {
            ApiResponse response = await _masterService.GetAllEquipments();
            response.Complete();
            return Ok(response);

        }

        [HttpGet]
        [Route("GetEquipmentById/{equipmentId}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] string equipmentId)
        {
            ApiResponse response = await _masterService.GetEquipmentsById(equipmentId);
            response.Complete();
            return Ok(response);
        }

        [HttpPost]
        [Route("AddNewEquipment")]
        public async Task<IActionResult> AddNewEquipment(AddEquipmentDto equipmentDto)
        {
            ApiResponse response = await _masterService.AddNewEquipment(equipmentDto);
            response.Complete();
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteEquipment/{equipmentId}")]
        public async Task<IActionResult> DeleteEquipment([FromRoute] string equipmentId)
        {
            ApiResponse response = await _masterService.DeleteEquipment(equipmentId);
            response.Complete();
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateEquipment/{equipmentId}")]
        public async Task<IActionResult> UpdateEquipment( [FromRoute] string equipmentId, [FromBody] AddEquipmentDto equipmentModel)
        {
            ApiResponse response = await _masterService.UpdateEquipment(equipmentId, equipmentModel);
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCountriesList")]
        public async Task<IActionResult> GetCountriesList()
        {
            ApiResponse response = await _masterService.GetAllCountries();
            response.Complete();
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllExercise")]
        public async Task<IActionResult> GetAllExercise()
        {
            ApiResponse response = await _masterService.GetAllExercise();
            response.Complete();
            return Ok(response);
        }

        [HttpPost]
        [Route("AddNewExercise")]
        public async Task<IActionResult> AddNewExercise([FromBody] ExerciseDtoModel exerciseModel)
        {
            ApiResponse response = await _masterService.AddNewExercise(exerciseModel);
            response.Complete();
            return Ok(response);
        }

        [HttpDelete("DeleteExercise/{exerciseId}")]
        public async Task<IActionResult> DeleteExercise([FromRoute] string exerciseId)
        {
            ApiResponse response = await _masterService.DeleteExercise(exerciseId);
            response.Complete();
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateExercise/{exerciseId}")]
        public async Task<IActionResult> UpdateExercise([FromRoute] string exerciseId, [FromBody] ExerciseDtoModel exerciseModel)
        {
            ApiResponse response = await _masterService.UpdateExercise(exerciseId, exerciseModel);
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetExerciseById/{exerciseId}")]
        public async Task<IActionResult> GetExercisrById([FromRoute] string exerciseId)
        {
            ApiResponse response = await _masterService.GetExerciseById(exerciseId);
            response.Complete();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllPackages")]
        public async Task<IActionResult> GetAllPackages()
        {
            ApiResponse response = await _masterService.GetAllPackages();
            response.Complete();
            return Ok(response);
        }

        [HttpGet("GetPackagesById/{packageId}")]
        public async Task<IActionResult> GetPackagesById([FromRoute] string packageId)
        {
            ApiResponse response = await _masterService.GetPackageById(packageId);
            response.Complete();
            return Ok(response);
        }

        [HttpPost("upsertPackage")]
        public async Task<IActionResult> UpsertPackage([FromBody] AddPackagesDto packageDto)
        {
            var response = await _masterService.UpsertPackage(packageDto);
            response.Complete();
            return Ok(response);
        }


        [HttpDelete]
        [Route("DeletePackages/{packageId}")]
        public async Task<IActionResult> DeletePackages([FromRoute] string packageId)
        {
            ApiResponse response = await _masterService.DeletePackage(packageId);
            response.Complete();
            return Ok(response);
        }

    }
}
