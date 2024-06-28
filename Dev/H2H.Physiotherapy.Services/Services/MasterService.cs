using AutoMapper;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DatabaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Features;
using System;
using H2H.Physiotherapy.Services.Logging;
using H2H.Physiotherapy.Services.Request;

using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterStore _masterStore;
        private readonly IMapper _mapper;
      
        public MasterService(IMasterStore masterStore , IMapper mapper)
        {
            _masterStore = masterStore;
            _mapper = mapper;
        }
        public async Task<ApiResponse> GetAllEquipments()
        {
            ApiResponse Response = new ApiResponse();
            var equipments = await _masterStore.GetAllEquipment();
            Response.Response =  _mapper.Map<List<EquipmentDto>>(equipments);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched All Equipments";
         
            return Response;
        }

        public async Task<ApiResponse> GetEquipmentsById(string equipmentId)
        {
            ApiResponse Response = new ApiResponse();
           var equipmentById = await _masterStore.GetEquipmentById(equipmentId);
                Response.Response = _mapper.Map<List<EquipmentDto>>(equipmentById);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully fetched Equipment by Id";
            return Response;

        }

        public async Task<ApiResponse> AddNewEquipment(AddEquipmentDto equipment)
        {
            ApiResponse Response = new ApiResponse();
            await _masterStore.AddNewEquipment(equipment);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully added new Equipment";
            
          
            return Response;
        }

        public async Task<ApiResponse> DeleteEquipment(string equipmentId)
        {
            ApiResponse Response = new ApiResponse();
           
                await _masterStore.DeleteEquipment(equipmentId);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully deleted Equipment";
            return Response;
        }

        public async Task<ApiResponse> UpdateEquipment(string equipmentId, AddEquipmentDto equipmentDto)
        {
            ApiResponse Response = new ApiResponse();
            
                await _masterStore.UpdateEquipment(equipmentId, equipmentDto);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully updated the Equipment";
            return Response;
        }


        public async Task<ApiResponse> GetAllPhysiotheraphy()
        {
            ApiResponse Response = new ApiResponse();

            var res = await _masterStore.GetAllPhysiotheraphyType();
            Response.Response = _mapper.Map<List<PhysiotherapyTypeDto>>(res);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched PhysiotheraphyType";

            return Response;
        }

        public async Task<ApiResponse> GetAllRoles()
        {
            ApiResponse Response = new ApiResponse();
                var roles = await _masterStore.GetAllRoles();
                Response.Response = _mapper.Map<List<RolesModelDTO>>(roles);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully fetched Roles";

            return Response;
        }

        public async Task<ApiResponse> GetAllExercise()
        {
            ApiResponse Response = new ApiResponse();

                var resp = await _masterStore.GetAllExercise();
                Response.Response = _mapper.Map<List<ExerciseDtoModel>>(resp);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully fetched Exercise Details";
                     
            return Response;
        }

        public async Task<ApiResponse> AddNewExercise(ExerciseDtoModel exercise)
        {
            ApiResponse Response = new ApiResponse();
            
                await _masterStore.AddNewExercise(exercise);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully added new Exercise";
            
            
            return Response;
        }

        public async Task<ApiResponse> UpdateExercise( string exerciseId , ExerciseDtoModel exercise)
        {
            ApiResponse Response = new ApiResponse();
            
                await _masterStore.UpdateExercise(exerciseId, exercise);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully updated the Exercise";
           
            return Response;
        }

        public async Task<ApiResponse> DeleteExercise(string exerciseId)
        {
            ApiResponse Response = new ApiResponse();
            
                await _masterStore.DeleteExercise(exerciseId);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully deleted Exercise";
            
           
            return Response;
        }

        public async Task<ApiResponse> GetExerciseById(string exerciseId)
        {
            ApiResponse Response = new ApiResponse();
                var res = await _masterStore.GetExerciseById(exerciseId);
                if(res == null || !res.Any())
                {
                Response.Message = "ExerciseId is not found are is currently inactive";
                Response.ResponseStatus = "success";
                }
                else
            {
                Response.Response = _mapper.Map<List<ExerciseDtoModel>>(res);
                Response.ResponseStatus = "success";
                Response.Message = "Successfully got the Exercise by Id";
            }
                        
            return Response;
        }  


        public async Task<ApiResponse> GetAllEmirates()
        {
            ApiResponse response = new ApiResponse();

            response.Response = await _masterStore.GetAllEmiratesAsync();
            response.ResponseStatus = "success";
            response.Message = "Emirates retrieved successfully";

            return response;
        }

        public async Task<ApiResponse> GetAllEnquiryType()
        {
            ApiResponse Response = new ApiResponse();

            Response.Response = await _masterStore.GetAllEnquiryType();
            Response.ResponseStatus = "success";
            Response.Message = "All Enquiry Types Retrieved";

            return Response;
        }

        public async Task<ApiResponse> GetAllServices()
        {
            ApiResponse Response = new ApiResponse();

            Response.Response = await _masterStore.GetAllServices();
            Response.ResponseStatus = "success";
            Response.Message = "All Services Retrieved";
            return Response;
        }

        public async Task<ApiResponse> GetServiceById(string id)
        {
            ApiResponse Response = new ApiResponse();
            Response.Response = await _masterStore.GetServiceById(id);
            Response.ResponseStatus = "success";
            Response.Message = "Service Retrieved Successfully";

            return Response;
        }

        public async Task<ApiResponse> AddService(ServiceDto dto)
        {
            ApiResponse Response = new ApiResponse();
            await _masterStore.AddService(dto);
            Response.ResponseStatus = "success";
            Response.Message = "Service Added Successfully";

            return Response;
        }

        public async Task<ApiResponse> UpdateService(string id, ServiceUpdateDto dto)
        {
            ApiResponse Response = new ApiResponse();
            await _masterStore.UpdateService(id, dto);
            Response.ResponseStatus = "success";
            Response.Message = "Updation Successful";

            return Response;
        }

        public async Task<ApiResponse> DeleteService(string id)
        {
            ApiResponse Response = new ApiResponse();
            await _masterStore.RemoveService(id);
            Response.ResponseStatus = "success";
            Response.Message = "Deletion Successfully";

            return Response;
        }


        public async Task<ApiResponse> GetAllCategories()
        {
            ApiResponse response = new ApiResponse();

            var categories = await _masterStore.GetAllCategories();
            response.Response = _mapper.Map<List<CategoryDto>>(categories);
            response.ResponseStatus = "success";
            response.Message = "Categories retrieved successfully";

            return response;
        }


        public async Task<ApiResponse> GetAllCountries()
        {
            ApiResponse Response = new ApiResponse();

            var res = await _masterStore.GetAllCountries();
            Response.Response = _mapper.Map<List<CountryDTO>>(res);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched Countries";

            return Response;
        }

        public async Task<ApiResponse> GetAllDevices()
        {
            ApiResponse response = new ApiResponse();
            response.Response = await _masterStore.GetAllDevicesAsync();
            response.ResponseStatus = "success";
            response.Message = "All devices retrieved successfully";

            return response;
        }

        public async Task<ApiResponse> GetDeviceByDeviceId(string deviceId)
        {
            ApiResponse response = new ApiResponse();
            response.Response = await _masterStore.GetDeviceByDeviceIdAsync(deviceId);
            response.ResponseStatus = "success";
            response.Message = "Devices retrieved successfully";

            return response;
        }

        public async Task<ApiResponse> DeleteDevice(string deviceId)
        {
            ApiResponse response = new ApiResponse();

            var deleted = await _masterStore.DeleteDeviceAsync(deviceId);
            response.Response = deleted;
            response.ResponseStatus = "success";
            response.Message = "Devices deleted successfully";

            return response;
        }

        public async Task<ApiResponse> UpsertDevice(DevicesDto deviceDto)
        {
            ApiResponse response = new ApiResponse();
            int updatedBy = 1;  
            var isUpdate = !string.IsNullOrEmpty(deviceDto.DeviceId);

            await _masterStore.UpsertDeviceAsync(deviceDto, updatedBy);

            response.ResponseStatus = "success";
            response.Message = "Saved Successfully";

            return response;
        }

        public async Task<ApiResponse> GetPackageById(string packageId)
        {
            ApiResponse Response = new ApiResponse();
            var packagesById = await _masterStore.GetPackagesById(packageId);
            Response.Response = _mapper.Map<List<PackageDto>>(packagesById); 
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched Packages by Id";

            return Response;

        }

        public async Task<ApiResponse> GetAllPackages()
        {
            ApiResponse Response = new ApiResponse();
            var packages = await _masterStore.GetAllPackages();
            Response.Response = _mapper.Map<List<PackageDto>>(packages);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully fetched all Packages";

            return Response;
        }

        public async Task<ApiResponse> UpsertPackage(AddPackagesDto packagesDto)
        {
            ApiResponse response = new ApiResponse();
            int updatedBy = 1;  
            var isUpdate = !string.IsNullOrEmpty(packagesDto.PackagesId);

            await _masterStore.UpsertPackageAsync(packagesDto, updatedBy);

            response.ResponseStatus = "success";
            response.Message = isUpdate ? "Package updated successfully" : "Package inserted successfully";

            return response;
        }

        public async Task<ApiResponse> DeletePackage(string packageId)
        {
            ApiResponse Response = new ApiResponse();
            await _masterStore.DeletePackages(packageId);
            Response.ResponseStatus = "success";
            Response.Message = "Successfully deleted Packages";

            return Response;


        }
    }
}
