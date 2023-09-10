﻿using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<FilterTuple, FilterDataTuple>().ReverseMap();
            CreateMap<GeneralDepartmentMaster, GeneralDepartmentModel>().ReverseMap();
            CreateMap<GeneralCountryMaster, GeneralCountryModel>().ReverseMap();
            CreateMap<GeneralTaxMaster, GeneralTaxMasterModel>().ReverseMap();
            CreateMap<GeneralTaxGroupMaster, GeneralTaxGroupModel>().ReverseMap();
            CreateMap<GeneralCityMaster, GeneralCityModel>().ReverseMap();
            CreateMap<GeneralNationalityMaster, GeneralNationalityModel>().ReverseMap();
            CreateMap<EmployeeDesignationMaster, GeneralDesignationModel>().ReverseMap();
            CreateMap<OrganisationCentreMaster, OrganisationCentreModel>().ReverseMap();
            CreateMap<AdminSanctionPost, AdminSanctionPostModel>().ReverseMap();
            CreateMap<AdminRoleMaster, AdminRoleModel>().ReverseMap();
        }
    }
}
