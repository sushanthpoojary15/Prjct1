using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Abstractions.IDataStore
{
    public interface IMasterStore
    {
        public Task<List<ExerciseModel>> GetAllExercise();
        public Task<List<Emirates>> GetAllEmiratesAsync();
        public Task<List<PhysiotherapyTypeModel>> GetAllPhysiotheraphyType();
         public Task<List<RolesModel>> GetAllRoles();
        public Task<List<EnquiryType>> GetAllEnquiryType();
        public Task<List<ServiceModel>> GetAllServices();
        public Task<ServiceModel> GetServiceById(string id);
        public Task AddService(ServiceDto service);
        public Task UpdateService(string id, ServiceUpdateDto service);
        public Task RemoveService(string id);
        public Task<List<PackagesModel>> GetAllPackages();
        public Task<List<PackagesModel>> GetPackagesById(string packageId);

        public  Task UpsertPackageAsync(AddPackagesDto packagesDto, int updatedBy);
        public Task DeletePackages(string packageId);
        public Task<List<Category>> GetAllCategories();
        public Task<List<CountryModel>> GetAllCountries();
        public Task AddNewExercise(ExerciseDtoModel exercise);
        public Task DeleteExercise(string exerciseId);
        public Task UpdateExercise(string exerciseId , ExerciseDtoModel exerciseDto);
       
        public Task<List<ExerciseModel>> GetExerciseById(string exerciseId);
       
        public Task<List<Devices>> GetAllDevicesAsync();
        public Task<Devices> GetDeviceByDeviceIdAsync(string deviceId);
        public Task<bool> DeleteDeviceAsync(string deviceId);
        public Task UpsertDeviceAsync(DevicesDto devicesDto, int updatedBy);

        public Task<List<EquipmentsModel>> GetAllEquipment();
        public Task<List<EquipmentsModel>> GetEquipmentById(string equipmentId);
        public Task AddNewEquipment(AddEquipmentDto equipmentDto);
        public Task DeleteEquipment(string equipmentId);
        public Task UpdateEquipment(string equipmentId, AddEquipmentDto equipmentDto);

    }
}