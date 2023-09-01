using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.API.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<GeneralDepartmentMaster, GeneralDepartmentModel>().ReverseMap();
            CreateMap<GeneralCountryMaster, GeneralCountryModel>().ReverseMap();
            CreateMap<GeneralTaxMaster, GeneralTaxMasterModel>().ReverseMap();
            CreateMap<GeneralTaxGroupMaster, GeneralTaxGroupModel>().ReverseMap();
            CreateMap<GeneralCityMaster, GeneralCityModel>().ReverseMap();
            CreateMap<GeneralNationalityMaster, GeneralNationalityModel>().ReverseMap();
            CreateMap<GeneralDesignationMaster, GeneralDesignationMasterModel>().ReverseMap();
            CreateMap<OrganisationCentreMaster, OrganisationCentreMasterModel>().ReverseMap();
        }
    }
}

