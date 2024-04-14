using AutoMapper;

using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;

namespace Coditech.API.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<FilterTuple, FilterDataTuple>().ReverseMap();
            CreateMap<GeneralPerson, GeneralPersonModel>().ReverseMap();

            CreateMap<AdminSanctionPost, AdminSanctionPostModel>().ReverseMap();
            CreateMap<AdminRoleMaster, AdminRoleModel>().ReverseMap();
            CreateMap<AdminRoleApplicableDetails, AdminRoleApplicableDetailsModel>().ReverseMap();

            CreateMap<GeneralDepartmentMaster, GeneralDepartmentModel>().ReverseMap();
            CreateMap<GeneralCountryMaster, GeneralCountryModel>().ReverseMap();
            CreateMap<GeneralEmailTemplate, GeneralEmailTemplateModel>().ReverseMap();
            CreateMap<GeneralFinancialYear, GeneralFinancialYearModel>().ReverseMap();
            CreateMap<GeneralTaxMaster, GeneralTaxMasterModel>().ReverseMap();
            CreateMap<GeneralTaxGroupMaster, GeneralTaxGroupModel>().ReverseMap();
            CreateMap<GeneralCityMaster, GeneralCityModel>().ReverseMap();
            CreateMap<GeneralNationalityMaster, GeneralNationalityModel>().ReverseMap();
            CreateMap<EmployeeDesignationMaster, GeneralDesignationModel>().ReverseMap();
            CreateMap<OrganisationCentreMaster, OrganisationCentreModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingMaster, OrganisationCentrewiseBuildingModel>().ReverseMap();
           
            CreateMap<OrganisationMaster, OrganisationModel>().ReverseMap();
            CreateMap<GeneralRegionMaster, GeneralRegionModel>().ReverseMap();
            CreateMap<OrganisationCentrePrintingFormat, OrganisationCentrePrintingFormatModel>().ReverseMap();
            CreateMap<GeneralEnumaratorGroup, GeneralEnumaratorGroupModel>().ReverseMap();
            CreateMap<GeneralEnumaratorMaster, GeneralEnumaratorModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseGSTCredential, OrganisationCentrewiseGSTCredentialModel>().ReverseMap();
            CreateMap<GeneralPerson, GeneralPersonModel>().ReverseMap();
            CreateMap<GeneralSystemGlobleSettingMaster, GeneralSystemGlobleSettingModel>().ReverseMap();
            CreateMap<GeneralOccupationMaster, GeneralOccupationModel>().ReverseMap();
            CreateMap<GeneralMeasurementUnitMaster, GeneralMeasurementUnitModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseDepartment, OrganisationCentrewiseDepartmentModel>().ReverseMap();
            CreateMap<GeneralRunningNumbers, GeneralRunningNumbersModel>().ReverseMap();
            CreateMap<GeneralLeadGenerationMaster, GeneralLeadGenerationModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingRooms, OrganisationCentrewiseBuildingRoomsModel>().ReverseMap();
        }
    }
}
