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
            CreateMap<AdminRoleMenuDetailsModel, AdminRoleMenuDetailsViewModel>().ReverseMap();
            CreateMap<AdminRoleApplicableDetailsModel, AdminRoleApplicableDetailsViewModel>().ReverseMap();
            CreateMap<AdminRoleApplicableDetailsListModel, AdminRoleApplicableDetailsListViewModel>().ReverseMap();
            #endregion

            #region General Master
            CreateMap<GeneralDepartmentModel, GeneralDepartmentViewModel>().ReverseMap();
            CreateMap<GeneralDepartmentListModel, GeneralDepartmentListViewModel>().ReverseMap();
            CreateMap<GeneralCountryModel, GeneralCountryViewModel>().ReverseMap();
            CreateMap<GeneralFinancialYearModel, GeneralFinancialYearViewModel>().ReverseMap();
            CreateMap<GeneralCountryListModel, GeneralCountryListViewModel>().ReverseMap();
            CreateMap<GeneralFinancialYearListModel, GeneralFinancialYearListViewModel>().ReverseMap();
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
            CreateMap<GeneralPersonAddressModel, GeneralPersonAddressViewModel>().ReverseMap();
            CreateMap<GeneralPersonAddressListModel, GeneralPersonAddressListViewModel>().ReverseMap();
            CreateMap<GeneralRunningNumbersModel, GeneralRunningNumbersViewModel>().ReverseMap();
            CreateMap<GeneralRunningNumbersListModel, GeneralRunningNumbersListViewModel>().ReverseMap();
            CreateMap<GeneralLeadGenerationModel, GeneralLeadGenerationViewModel>().ReverseMap();
            CreateMap<GeneralLeadGenerationListModel, GeneralLeadGenerationListViewModel>().ReverseMap();
            CreateMap<GeneralLeadGenerationViewModel, GeneralPersonModel>().ReverseMap();
            #endregion

            #region Organisation
            CreateMap<OrganisationModel, OrganisationMasterViewModel>().ReverseMap();
            CreateMap<OrganisationCentreModel, OrganisationCentreViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingModel, OrganisationCentrewiseBuildingViewModel>().ReverseMap();
            CreateMap<OrganisationCentreListModel, OrganisationCentreListViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingListModel, OrganisationCentrewiseBuildingListViewModel>().ReverseMap();
            CreateMap<OrganisationCentrePrintingFormatModel, OrganisationCentrePrintingFormatViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseGSTCredentialModel, OrganisationCentrewiseGSTCredentialViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseDepartmentModel, OrganisationCentrewiseDepartmentViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseDepartmentListModel, OrganisationCentrewiseDepartmentListViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingRoomsModel, OrganisationCentrewiseBuildingRoomsViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingRoomsListModel, OrganisationCentrewiseBuildingRoomsListViewModel>().ReverseMap();
            #endregion

            #region Employee            
            CreateMap<EmployeeCreateEditViewModel, GeneralPersonViewModel>().ReverseMap();
            CreateMap<EmployeeCreateEditViewModel, GeneralPersonModel>().ReverseMap();
            CreateMap<EmployeeMasterListModel, EmployeeMasterListViewModel>().ReverseMap();
            CreateMap<EmployeeMasterModel, EmployeeMasterViewModel>().ReverseMap();
            #endregion

            #region Gym
            CreateMap<GymCreateEditMemberViewModel, GeneralPersonModel>().ReverseMap();
            CreateMap<GymMemberFollowUpListViewModel, GymMemberFollowUpListModel>().ReverseMap();
            CreateMap<GymMemberFollowUpViewModel, GymMemberFollowUpModel>().ReverseMap();
            CreateMap<GymBodyMeasurementTypeModel, GymBodyMeasurementTypeViewModel>().ReverseMap();
            CreateMap<GymBodyMeasurementTypeListModel, GymBodyMeasurementTypeListViewModel>().ReverseMap();
            CreateMap<GymMembershipPlanModel, GymMembershipPlanViewModel>().ReverseMap();
            CreateMap<GymMembershipPlanListModel, GymMembershipPlanListViewModel>().ReverseMap();
            CreateMap<EmployeeCreateEditViewModel, EmployeeMasterModel>().ReverseMap();
            CreateMap<GymMemberBodyMeasurementModel, GymMemberBodyMeasurementViewModel>().ReverseMap();
            CreateMap<GymMemberBodyMeasurementListModel, GymMemberBodyMeasurementListViewModel>().ReverseMap();
            CreateMap<GeneralPersonAttendanceDetailsListViewModel, GeneralPersonAttendanceDetailsListModel>().ReverseMap();
            CreateMap<GeneralPersonAttendanceDetailsViewModel, GeneralPersonAttendanceDetailsModel>().ReverseMap();
            CreateMap<GymMemberMembershipPlanModel, GymMemberMembershipPlanViewModel>().ReverseMap();
            CreateMap<GymMemberMembershipPlanListModel, GymMemberMembershipPlanListViewModel>().ReverseMap();
            CreateMap<GymMemberSalesInvoiceModel, GymMemberSalesInvoiceViewModel>().ReverseMap();
            CreateMap<GymMemberSalesInvoiceListModel, GymMemberSalesInvoiceListViewModel>().ReverseMap();
            #endregion

            #region Person
            CreateMap<GeneralPersonFollowUpListViewModel, GeneralPersonFollowUpListModel>().ReverseMap();
            CreateMap<GeneralPersonFollowUpViewModel, GeneralPersonFollowUpModel>().ReverseMap();
            #endregion

            #region HMS
            CreateMap<HospitalDoctorsModel, HospitalDoctorsViewModel>().ReverseMap();
            CreateMap<HospitalDoctorsListModel, HospitalDoctorsListViewModel>().ReverseMap();
            #endregion
        }
    }
}
