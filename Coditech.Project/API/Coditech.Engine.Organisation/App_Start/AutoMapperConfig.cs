using AutoMapper;

using Coditech.API.Data;
using Coditech.API.Data.DataModel.Gym;
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
            CreateMap<AdminRoleMediaFolderAction, AdminRoleMediaFolderActionModel>().ReverseMap();
            CreateMap<AdminRoleMediaFolders, AdminRoleMediaFoldersModel>().ReverseMap();

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
            CreateMap<GeneralSystemGlobleSettingMaster, GeneralSystemGlobleSettingModel>().ReverseMap();
            CreateMap<GeneralOccupationMaster, GeneralOccupationModel>().ReverseMap();
            CreateMap<GeneralMeasurementUnitMaster, GeneralMeasurementUnitModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseDepartment, OrganisationCentrewiseDepartmentModel>().ReverseMap();
            CreateMap<GeneralRunningNumbers, GeneralRunningNumbersModel>().ReverseMap();
            CreateMap<GeneralLeadGenerationMaster, GeneralLeadGenerationModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseBuildingRooms, OrganisationCentrewiseBuildingRoomsModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseSmtpSetting, OrganisationCentrewiseSmtpSettingModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseEmailTemplate, OrganisationCentrewiseEmailTemplateModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseUserNameRegistration, OrganisationCentrewiseUserNameRegistrationModel>().ReverseMap();
            CreateMap<UserMainMenuMaster, UserMainMenuModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseSmsSetting, OrganisationCentrewiseSmsSettingModel>().ReverseMap();
            CreateMap<CoditechApplicationSetting, CoditechApplicationSettingModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseWhatsAppSetting, OrganisationCentrewiseWhatsAppSettingModel>().ReverseMap();
            CreateMap<LogMessage, LogMessageModel>().ReverseMap();
            CreateMap<GeneralSmsProvider, GeneralSmsProviderModel>().ReverseMap();
            CreateMap<GeneralWhatsAppProvider, GeneralWhatsAppProviderModel>().ReverseMap();
            CreateMap<GeneralDistrictMaster, GeneralDistrictModel>().ReverseMap();
            CreateMap<GeneralNotification, GeneralNotificationModel>().ReverseMap();
            CreateMap<GeneralTrainerMaster, GeneralTrainerModel>().ReverseMap();
            CreateMap<GeneralTraineeAssociatedToTrainer, GeneralTraineeAssociatedToTrainerModel>().ReverseMap();
            CreateMap<GeneralBatchMaster, GeneralBatchModel>().ReverseMap();
            CreateMap<GeneralBatchUser, GeneralBatchUserModel>().ReverseMap();
            CreateMap<TaskMaster, TaskMasterModel>().ReverseMap();
            CreateMap<TaskApprovalSetting, TaskApprovalSettingModel>().ReverseMap();
            CreateMap<TicketMaster, TicketMasterModel>().ReverseMap();
            CreateMap<TicketDetails, TicketDetailsModel>().ReverseMap();

            #region User
            CreateMap<UserMaster, UserModel>().ReverseMap();
            CreateMap<UserModuleMaster, UserModuleModel>().ReverseMap();
            CreateMap<UserMaster, GeneralPersonModel>().ReverseMap();
            CreateMap<GeneralPersonAddress, GeneralPersonAddressModel>().ReverseMap();
            CreateMap<GeneralPersonFollowUp, GeneralPersonFollowUpModel>().ReverseMap();
            CreateMap<GeneralPersonAttendanceDetails, GeneralPersonAttendanceDetailsModel>().ReverseMap();
            #endregion

            #region Employee
            CreateMap<EmployeeMaster, EmployeeMasterModel>().ReverseMap();
            CreateMap<EmployeeService, EmployeeServiceModel>().ReverseMap();
            #endregion

            #region Accounts
            CreateMap<AccGLSetupNarration, AccGLSetupNarrationModel>().ReverseMap();
            #endregion
        }
    }
}
