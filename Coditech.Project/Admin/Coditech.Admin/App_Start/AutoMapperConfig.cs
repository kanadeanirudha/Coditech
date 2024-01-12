using AutoMapper;

using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Model;

namespace Coditech.Admin
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region Admin 
            CreateMap<AdminSanctionPostModel, AdminSanctionPostViewModel>().ReverseMap();
            CreateMap<AdminRoleModel, AdminRoleViewModel>().ReverseMap();
            #endregion

            #region General Master
            CreateMap<GeneralDepartmentModel, GeneralDepartmentViewModel>().ReverseMap();
            CreateMap<GeneralDepartmentListModel, GeneralDepartmentListViewModel>().ReverseMap();
            CreateMap<GeneralCountryModel, GeneralCountryViewModel>().ReverseMap();
            CreateMap<GeneralCountryListModel, GeneralCountryListViewModel>().ReverseMap();
            CreateMap<GeneralNationalityModel, GeneralNationalityViewModel>().ReverseMap();
            CreateMap<GeneralNationalityListModel, GeneralNationalityListViewModel>().ReverseMap();
            CreateMap<GeneralDesignationModel, GeneralDesignationViewModel>().ReverseMap();
            CreateMap<GeneralDesignationListModel, GeneralDesignationListViewModel>().ReverseMap();
            CreateMap<GeneralCityModel, GeneralCityViewModel>().ReverseMap();
            CreateMap<GeneralCityListModel, GeneralCityListViewModel>().ReverseMap();
            CreateMap<GeneralTaxMasterModel, GeneralTaxMasterViewModel>().ReverseMap();
            CreateMap<GeneralTaxMasterListModel, GeneralTaxMasterListViewModel>().ReverseMap();
            CreateMap<GeneralTaxGroupModel, GeneralTaxGroupMasterViewModel>().ReverseMap();
            CreateMap<GeneralTaxGroupMasterListModel, GeneralTaxGroupMasterListViewModel>().ReverseMap();
            CreateMap<GeneralRegionModel, GeneralRegionViewModel>().ReverseMap();
            CreateMap<GeneralRegionListModel, GeneralRegionListViewModel>().ReverseMap();
            CreateMap<GeneralPersonModel, GeneralPersonViewModel>().ReverseMap();
            CreateMap<GeneralSystemGlobleSettingModel, GeneralSystemGlobleSettingViewModel>().ReverseMap();
            CreateMap<GeneralSystemGlobleSettingListModel, GeneralSystemGlobleSettingListViewModel>().ReverseMap();
            CreateMap<GymMemberDetailsModel, GymMemberDetailsViewModel>().ReverseMap();
            CreateMap<GymMemberDetailsListModel, GymMemberDetailsListViewModel>().ReverseMap();
            CreateMap<GeneralEnumaratorGroupListModel, GeneralEnumaratorGroupViewModel>().ReverseMap();
            CreateMap<GeneralEnumaratorGroupModel, GeneralEnumaratorGroupViewModel>().ReverseMap();
            CreateMap<GeneralEnumaratorModel, GeneralEnumaratorViewModel>().ReverseMap();
            CreateMap<GeneralOccupationModel, GeneralOccupationViewModel>().ReverseMap();
            CreateMap<GeneralOccupationListModel, GeneralOccupationListViewModel>().ReverseMap();
            CreateMap<GeneralMeasurementUnitModel, GeneralMeasurementUnitViewModel>().ReverseMap();
            CreateMap<GeneralMeasurementUnitListModel, GeneralMeasurementUnitListViewModel>().ReverseMap();
            #endregion

            #region Organisation
            CreateMap<OrganisationModel, OrganisationMasterViewModel>().ReverseMap();
            CreateMap<OrganisationCentreModel, OrganisationCentreViewModel>().ReverseMap();
            CreateMap<OrganisationCentreListModel, OrganisationCentreListViewModel>().ReverseMap();
            CreateMap<OrganisationCentrePrintingFormatModel, OrganisationCentrePrintingFormatViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseGSTCredentialModel, OrganisationCentrewiseGSTCredentialViewModel>().ReverseMap();
            #endregion

            #region Gym
            CreateMap<GymCreateEditMemberViewModel, GeneralPersonModel>().ReverseMap();
            
            #endregion
        }
    }
}
