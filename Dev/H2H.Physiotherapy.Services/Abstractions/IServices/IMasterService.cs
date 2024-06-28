using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IServices
{
    public interface IMasterService
    {
        public Task<ApiResponse> GetAllPhysiotheraphy();
        public Task<ApiResponse> GetAllRoles();
        public Task<ApiResponse> GetAllEmirates();
        public Task<ApiResponse> GetAllEnquiryType();
        public Task<ApiResponse> GetAllServices();
        public Task<ApiResponse> GetServiceById(string id);
        public Task<ApiResponse> AddService(ServiceDto dto);
        public Task<ApiResponse> UpdateService(string id, ServiceUpdateDto dto);
        public Task<ApiResponse> DeleteService(string id);
        public Task<ApiResponse> GetAllCategories();
        public Task<ApiResponse> GetAllCountries();
        public Task<ApiResponse> GetAllExercise();
        public Task<ApiResponse> AddNewExercise(ExerciseDtoModel exercise);
        public Task<ApiResponse> DeleteExercise(string exerciseId);

        public Task<ApiResponse> UpdateExercise(string exerciseId , ExerciseDtoModel exercise);
        public Task<ApiResponse> GetExerciseById(string exerciseId);
     
        public Task<ApiResponse> GetAllDevices();
        public Task<ApiResponse> GetDeviceByDeviceId(string deviceId);
        public Task<ApiResponse> DeleteDevice(string deviceId); 
        public Task<ApiResponse> UpsertDevice(DevicesDto devices);


        public Task<ApiResponse> GetAllEquipments();
        public Task<ApiResponse> GetEquipmentsById(string equipmentId);
        public Task<ApiResponse> AddNewEquipment(AddEquipmentDto equipment);
        public Task<ApiResponse> DeleteEquipment(string equipmentId);
        public Task<ApiResponse> UpdateEquipment(string equipmentId, AddEquipmentDto equipmentDto);
        public Task<ApiResponse> GetPackageById(string packageId);
        public Task<ApiResponse> GetAllPackages();

        public Task<ApiResponse> UpsertPackage(AddPackagesDto addPackagesDto);
        public Task<ApiResponse> DeletePackage(string packageId);
    }
}
