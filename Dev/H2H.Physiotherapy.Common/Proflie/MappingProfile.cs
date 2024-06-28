using AutoMapper;
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.BaseModels;
using H2H.Physiotherapy.Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Common.Proflie
{
    public  class MappingProfile:Profile
    {
    
        public MappingProfile()
        {
            //CreateMap<UserBaseData, UserData>();
            CreateMap<CountryModel, CountryDTO>();
            CreateMap<RolesModel, RolesModelDTO>();   
            CreateMap<PhysiotherapyTypeModel, PhysiotherapyTypeDto>();
            CreateMap<ExerciseModel,ExerciseDtoModel>();
            CreateMap<EquipmentsModel,AddEquipmentDto>();
            CreateMap<EquipmentsModel,EquipmentDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Devices, DevicesDto>();
            CreateMap<UserBaseData, UserModel>();
            CreateMap<PackagesModel,AddPackagesDto>();
            CreateMap<PackagesModel,PackageDto>();
            CreateMap<Enquiry, EnquiryDTO>();
            
           
        }
    }
}