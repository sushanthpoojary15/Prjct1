using AutoMapper;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Data.Abstractions;
using H2H.Physiotherapy.Services.Abstractions.IDataStore;
using H2H.Physiotherapy.Services.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Data.Datastore
{
    public class MasterStore : IMasterStore
    {
        private readonly IDatabaseManager _databaseManager;
       
        public MasterStore(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;   
        }

        public async Task<List<PhysiotherapyTypeModel>> GetAllPhysiotheraphyType()
        {
            string commandText = "usp_GetPhysiotherapyType";

            var phsiotheraphyTypeList = await _databaseManager.GetAllColumns<PhysiotherapyTypeModel>(commandText);

            return phsiotheraphyTypeList.ToList();
        }

        public async Task<List<RolesModel>> GetAllRoles()
        {
            string commandText = "usp_GetAllRoles";

            var rolesInfoList = await _databaseManager.GetAllColumns<RolesModel>(commandText);
            return rolesInfoList.ToList();
        }

        public async Task AddNewExercise(ExerciseDtoModel exercise)
        {
            string commandText = "usp_AddmstExercise";

            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@Name", exercise.Name, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@CreatedBy", 1, SqlDbType.Int),
                _databaseManager.CreateParameter("@UpdatedBy", 1 , SqlDbType.Int)
            };

            await _databaseManager.Insert(commandText, parameters);

        }

        public async Task DeleteExercise(string exerciseId)
        {
            string deleteText = "usp_DeleteExercise";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@ExerciseId", exerciseId, SqlDbType.NVarChar)
            };
            await _databaseManager.Delete(deleteText, parameters);
        }

        public async Task<List<ExerciseModel>> GetAllExercise()
        {
            string commandText = "usp_GetAllExercise";

            var exerciseList = await _databaseManager.GetAllColumns<ExerciseModel>(commandText);
            return exerciseList.ToList();
        }

        public async Task<List<ExerciseModel>> GetExerciseById(string exerciseId)
        {
            string getExerciseById = "usp_GetExerciseById";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@ExerciseId", exerciseId, SqlDbType.NVarChar),
            };

            var exerciseList = await _databaseManager.Select<ExerciseModel>(getExerciseById, parameters);
            return exerciseList.ToList();

        }

        public async Task UpdateExercise(string exerciseId, ExerciseDtoModel exerciseDto)
        {
            string commandText = "usp_UpdateExercise";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@ExerciseId", exerciseId, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@Name", exerciseDto.Name, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@UpdatedBy", 1 , SqlDbType.Int),

            };

            await _databaseManager.Update(commandText, parameters);

        }

        public async Task<List<Emirates>> GetAllEmiratesAsync()
        {
            string commandText = "usp_GetAllEmirates";
            var emirates = await _databaseManager.GetAllColumns<Emirates>(commandText);
            return emirates.ToList();
        }

        public async Task<List<EnquiryType>> GetAllEnquiryType()
        {
            string commandText = "usp_GetAllEnquiryType";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@IsActive", true, SqlDbType.Bit),
            };
            var enquiryTypeList = await _databaseManager.GetAllColumns<EnquiryType>(commandText, parameters);
            return enquiryTypeList.ToList();
        }

        public async Task<List<ServiceModel>> GetAllServices()
        {
            string commandText = "usp_GetAllServices";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@IsActive", true, SqlDbType.Bit),
            };
            var serviceList = await _databaseManager.GetAllColumns<ServiceModel>(commandText, parameters);
            return serviceList.ToList();
        }

        public async Task<ServiceModel> GetServiceById(string id)
        {
            string fetchCommandText = "usp_GetServiceById";
            IDbDataParameter[] fetchParameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@ServiceId", id, SqlDbType.NVarChar)
            };

            var service = (await _databaseManager.GetAllColumns<ServiceModel>(fetchCommandText, fetchParameters)).FirstOrDefault();
            return service;
        }

        public async Task AddService(ServiceDto service)
        {
            string commandText = "usp_AddService";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@PatientType", service.PatientType, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@ServiceDuration", service.ServiceDuration, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@BeforeTravelTime", service.BeforeTravelTime, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@AfterTravelTime", service.AfterTravelTime, SqlDbType.NVarChar),

            };

            await _databaseManager.Insert(commandText, parameters);
        }

        public async Task UpdateService(string id, ServiceUpdateDto service)
        {
            string commandText = "usp_UpdateService";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@ServiceId", id, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@ServiceDuration", service.ServiceDuration, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@BeforeTravelTime", service.BeforeTravelTime, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@AfterTravelTime", service.AfterTravelTime, SqlDbType.NVarChar),
                //_databaseManager.CreateParameter("@UpdatedOn", service.UpdatedOn, SqlDbType.DateTime),
                _databaseManager.CreateParameter("@UpdatedBy", service.UpdatedBy, SqlDbType.Int)
            };

            await _databaseManager.Update(commandText, parameters);

        }

        public async Task RemoveService(string serviceId)
        {
            string commandText = "usp_SoftDeleteService";
            IDbDataParameter[] parameters =
            {
                _databaseManager.CreateParameter("@ServiceId", serviceId, SqlDbType.NVarChar)
            };

            await _databaseManager.Update(commandText, parameters);
        }


        public async Task<List<EquipmentsModel>> GetAllEquipment()
        {
            string commandText = "usp_GetAllEquipment";

            var exerciseList = await _databaseManager.GetAllColumns<EquipmentsModel>(commandText);
            return exerciseList.ToList();

        }

        public async Task<List<EquipmentsModel>> GetEquipmentById(string equipmentId)
        {
            string getEquipmentById = "usp_GetEquipmentById";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@EquipmentId", equipmentId, SqlDbType.NVarChar),
            };

            var equipmentList = await _databaseManager.Select<EquipmentsModel>(getEquipmentById, parameters);
            return equipmentList.ToList();

        }

        public async Task AddNewEquipment(AddEquipmentDto equipment)
        {
            string commandText = "usp_AddmstEquipment";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@EquipmentName", equipment.EquipmentName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@CreatedBy", 1, SqlDbType.Int),
                _databaseManager.CreateParameter("@UpdatedBy", 1, SqlDbType.Int)
            };

            await _databaseManager.Insert(commandText, parameters);



        }

        public async Task DeleteEquipment(string equipmentId)
        {
            string deleteText = "usp_DeleteEquipment";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@EquipmentId", equipmentId, SqlDbType.NVarChar)
            };
            await _databaseManager.Delete(deleteText, parameters);
        }

        public async Task UpdateEquipment(string equipmentId, AddEquipmentDto equipmentDto)
        {
            string commandText = "usp_UpdateEquipment";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@EquipmentId", equipmentId, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@EquipmentName", equipmentDto.EquipmentName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@UpdatedBy", 1, SqlDbType.Int)
            };
            await _databaseManager.Update(commandText, parameters);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            string commandText = "usp_GetAllCategories";
            var categories = await _databaseManager.GetAllColumns<Category>(commandText);
            return categories.ToList();
        }


        public async Task<List<CountryModel>> GetAllCountries()
        {
            string commandText = "usp_GetAllCountries";

            var countriesInfoList = await _databaseManager.GetAllColumns<CountryModel>(commandText);
            return countriesInfoList.ToList();
        }

        public async Task<List<Devices>> GetAllDevicesAsync()
        {
            string commandText = "usp_GetAllDevices";
            var devices = await _databaseManager.GetAllColumns<Devices>(commandText);
            return devices.ToList();
        }

        public async Task<Devices> GetDeviceByDeviceIdAsync(string deviceId)
        {
            string commandText = "usp_GetDeviceByDeviceId";
            var parameters = new IDbDataParameter[]
            {
            _databaseManager.CreateParameter("@DeviceId", deviceId, SqlDbType.NVarChar)
            };
            var devices = await _databaseManager.GetAllColumns<Devices>(commandText, parameters);
            return devices.FirstOrDefault();
        }
        public async Task<bool> DeleteDeviceAsync(string deviceId)
        {
            string commandText = "usp_DeleteDevice";
            var parameters = new IDbDataParameter[]
            {
            _databaseManager.CreateParameter("@DeviceId", deviceId, SqlDbType.NVarChar)
            };

            await _databaseManager.Delete(commandText, parameters);
            return true;
        }

        public async Task UpsertDeviceAsync(DevicesDto devicesDto, int updatedBy)
        {
            string commandText = "usp_UpsertDevice";
            var parameters = new IDbDataParameter[]
            {
              _databaseManager.CreateParameter("@DeviceId", string.IsNullOrEmpty(devicesDto.DeviceId) ? (object)DBNull.Value : devicesDto.DeviceId, SqlDbType.NVarChar),
              _databaseManager.CreateParameter("@Name", devicesDto.Name, SqlDbType.NVarChar),
              _databaseManager.CreateParameter("@UpdatedBy", updatedBy, SqlDbType.Int)
            };

            await _databaseManager.InsertOrUpdateWithTransaction(commandText, parameters);
        }

        public async Task<List<PackagesModel>> GetAllPackages()
        {
            string commandText = "usp_GetAllPackages";

            var packagesList = await _databaseManager.GetAllColumns<PackagesModel>(commandText);
            return packagesList.ToList();
        }

        public async Task<List<PackagesModel>> GetPackagesById(string packageId)
        {
            string commandText = "usp_GetPackagesById";

            IDbDataParameter[] parameters = new IDbDataParameter[]
           {
                _databaseManager.CreateParameter("@PackagesId", packageId, SqlDbType.NVarChar),
           };

            var packagesIdList = await _databaseManager.GetAllColumns<PackagesModel>(commandText, parameters);
            return packagesIdList.ToList();
        }

        public async Task UpsertPackageAsync(AddPackagesDto packagesDto, int updatedBy)
        {
            string commandText = "usp_UpsertPackage";
            var parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@PackagesId", string.IsNullOrEmpty(packagesDto.PackagesId) ? (object)DBNull.Value : packagesDto.PackagesId, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@PackageName", packagesDto.PackageName, SqlDbType.NVarChar),
                _databaseManager.CreateParameter("@Amount", packagesDto.Amount, SqlDbType.Int),
                _databaseManager.CreateParameter("@UpdatedBy", updatedBy, SqlDbType.Int),
                _databaseManager.CreateParameter("@EmiratesUUId" ,packagesDto.EmiratesId ,SqlDbType.NVarChar )
            };

            await _databaseManager.InsertOrUpdateWithTransaction(commandText, parameters);
        }

        public async Task DeletePackages(string packageId)
        {
            string deleteText = "usp_DeletePackages";
            IDbDataParameter[] parameters = new IDbDataParameter[]
            {
                _databaseManager.CreateParameter("@PackagesId", packageId, SqlDbType.NVarChar)
            };
            await _databaseManager.Delete(deleteText, parameters);
        }
    }
}
