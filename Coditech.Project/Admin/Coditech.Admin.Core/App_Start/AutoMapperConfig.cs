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
            #region Accounts
            CreateMap<UserProfileModel, UserProfileViewModel>().ReverseMap();
            CreateMap<AccGLSetupNarrationModel, AccGLSetupNarrationViewModel>().ReverseMap();
            CreateMap<AccGLSetupNarrationViewModel, AccGLSetupNarrationListViewModel>().ReverseMap();
            CreateMap<AccSetupMasterModel, AccSetupMasterViewModel>().ReverseMap();
            CreateMap<AccSetupMasterViewModel, AccSetupMasterListViewModel>().ReverseMap();
            CreateMap<AccSetupBalanceSheetModel, AccSetupBalanceSheetViewModel>().ReverseMap();
            CreateMap<AccSetupBalanceSheetViewModel, AccSetupBalanceSheetListViewModel>().ReverseMap();
            CreateMap<AccSetupBalanceSheetTypeModel, AccSetupBalanceSheetTypeViewModel>().ReverseMap();
            CreateMap<AccSetupBalanceSheetTypeViewModel, AccSetupBalanceSheetTypeListViewModel>().ReverseMap();
            CreateMap<AccSetupTransactionTypeModel, AccSetupTransactionTypeViewModel>().ReverseMap();
            CreateMap<AccSetupTransactionTypeViewModel, AccSetupTransactionTypeListViewModel>().ReverseMap();
            CreateMap<AccSetupGLBankModel, AccSetupGLBankViewModel>().ReverseMap();
            CreateMap<AccSetupGLBankViewModel, AccSetupGLBankListViewModel>().ReverseMap();
            CreateMap<AccSetupGLModel, AccSetupGLViewModel>().ReverseMap();
            CreateMap<AccSetupGLViewModel, AccSetupGLListViewModel>().ReverseMap();
            CreateMap<AccSetupChartOfAccountTemplateViewModel, AccSetupChartOfAccountTemplateListViewModel>().ReverseMap();
            CreateMap<AccGLTransactionModel, AccGLTransactionViewModel>().ReverseMap();
            CreateMap<AccGLTransactionViewModel, AccGLTransactionListViewModel>().ReverseMap();
            CreateMap<AccSetupGLTypeModel, AccSetupGLTypeViewModel>().ReverseMap();
            CreateMap<AccSetupGLTypeViewModel, AccSetupGLTypeListViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseAccountSetupModel, OrganisationCentrewiseAccountSetupViewModel>().ReverseMap();

            CreateMap<ACCGLOpeningBalanceModel, ACCGLOpeningBalanceViewModel>().ReverseMap();
            CreateMap<ACCGLOpeningBalanceViewModel, ACCGLOpeningBalanceListViewModel>().ReverseMap();
            CreateMap<AccGLIndividualOpeningBalanceModel, AccGLIndividualOpeningBalanceViewModel>().ReverseMap();
            CreateMap<AccPrequisiteModel, AccPrequisiteViewModel>().ReverseMap();
            CreateMap<AccountBalanceSheetReportModel, AccountBalanceSheetReportViewModel>().ReverseMap();
            CreateMap<AccountProfitAndLossReportModel, AccountProfitAndLossReportViewModel>().ReverseMap();
            #endregion

            #region Admin 
            CreateMap<AdminSanctionPostModel, AdminSanctionPostViewModel>().ReverseMap();
            CreateMap<AdminRoleModel, AdminRoleViewModel>().ReverseMap();
            CreateMap<AdminRoleMenuDetailsModel, AdminRoleMenuDetailsViewModel>().ReverseMap();
            CreateMap<AdminRoleApplicableDetailsModel, AdminRoleApplicableDetailsViewModel>().ReverseMap();
            CreateMap<AdminRoleApplicableDetailsListModel, AdminRoleApplicableDetailsListViewModel>().ReverseMap();
            CreateMap<AdminRoleMediaFolderActionModel, AdminRoleMediaFolderActionViewModel>().ReverseMap();
            CreateMap<AdminRoleMediaFoldersModel, AdminRoleMediaFoldersViewModel>().ReverseMap();
            #endregion

            #region General Master
            CreateMap<GeneralDepartmentModel, GeneralDepartmentViewModel>().ReverseMap();
            CreateMap<GeneralDepartmentListModel, GeneralDepartmentListViewModel>().ReverseMap();
            CreateMap<GeneralCountryModel, GeneralCountryViewModel>().ReverseMap();
            CreateMap<GeneralEmailTemplateModel, GeneralEmailTemplateViewModel>().ReverseMap();
            CreateMap<GeneralEmailTemplateListModel, GeneralEmailTemplateListViewModel>().ReverseMap();
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
            CreateMap<UserMainMenuModel, UserMainMenuViewModel>().ReverseMap();
            CreateMap<UserMainMenuListModel, UserMainMenuListViewModel>().ReverseMap();
            CreateMap<CoditechApplicationSettingModel, CoditechApplicationSettingViewModel>().ReverseMap();
            CreateMap<CoditechApplicationSettingListModel, CoditechApplicationSettingListViewModel>().ReverseMap();
            CreateMap<GeneralSmsProviderModel, GeneralSmsProviderViewModel>().ReverseMap();
            CreateMap<GeneralWhatsAppProviderListModel, GeneralWhatsAppProviderListViewModel>().ReverseMap();
            CreateMap<GeneralWhatsAppProviderModel, GeneralWhatsAppProviderViewModel>().ReverseMap();
            CreateMap<GeneralDistrictModel, GeneralDistrictViewModel>().ReverseMap();
            CreateMap<GeneralDistrictListModel, GeneralDistrictListViewModel>().ReverseMap();
            CreateMap<GeneralNotificationListModel, GeneralNotificationListViewModel>().ReverseMap();
            CreateMap<GeneralNotificationModel, GeneralNotificationViewModel>().ReverseMap();
            CreateMap<GeneralTrainerListModel, GeneralTrainerListViewModel>().ReverseMap();
            CreateMap<GeneralTrainerModel, GeneralTrainerViewModel>().ReverseMap();
            CreateMap<GeneralTraineeAssociatedToTrainerListModel, GeneralTraineeAssociatedToTrainerListViewModel>().ReverseMap();
            CreateMap<GeneralTraineeAssociatedToTrainerModel, GeneralTraineeAssociatedToTrainerViewModel>().ReverseMap();
            CreateMap<GeneralBatchListModel, GeneralBatchListViewModel>().ReverseMap();
            CreateMap<GeneralBatchModel, GeneralBatchViewModel>().ReverseMap();
            CreateMap<GeneralBatchUserListModel, GeneralBatchUserListViewModel>().ReverseMap();
            CreateMap<GeneralBatchUserModel, GeneralBatchUserViewModel>().ReverseMap();
            CreateMap<TaskMasterModel, TaskMasterViewModel>().ReverseMap();
            CreateMap<TaskApprovalSettingListModel, TaskApprovalSettingListViewModel>().ReverseMap();
            CreateMap<TaskApprovalSettingModel, TaskApprovalSettingViewModel>().ReverseMap();
            CreateMap<GeneralMessagesModel, GeneralMessagesViewModel>().ReverseMap();
            CreateMap<TicketMasterModel, TicketMasterViewModel>().ReverseMap();
            CreateMap<TicketMasterListModel, TicketMasterListViewModel>().ReverseMap();
            CreateMap<TicketDetailsModel, TicketMasterViewModel>().ReverseMap();
            CreateMap<TicketDetailsListModel, TicketDetailsListViewModel>().ReverseMap();
            CreateMap<GeneralCurrencyMasterModel, GeneralCurrencyMasterViewModel>().ReverseMap();
            CreateMap<GeneralCurrencyMasterListModel, GeneralCurrencyMasterListViewModel>().ReverseMap();
            CreateMap<TaskSchedulerModel, TaskSchedulerViewModel>().ReverseMap();
            CreateMap<TaskSchedulerListModel, TaskSchedulerListViewModel>().ReverseMap();
            CreateMap<UserModuleModel, UserModuleViewModel>().ReverseMap();
            CreateMap<UserModuleListModel, UserModuleListViewModel>().ReverseMap();
            CreateMap<GeneralPolicyModel, GeneralPolicyViewModel>().ReverseMap();
            CreateMap<GeneralPolicyListModel, GeneralPolicyListViewModel>().ReverseMap();
            CreateMap<GeneralPolicyRulesModel, GeneralPolicyRulesViewModel>().ReverseMap();
            CreateMap<GeneralPolicyRulesListModel, GeneralPolicyRulesListViewModel>().ReverseMap();
            CreateMap<GeneralPolicyDetailsModel, GeneralPolicyDetailsViewModel>().ReverseMap();
            CreateMap<GeneralPolicyDetailsListModel, GeneralPolicyDetailsListViewModel>().ReverseMap();
            #endregion
            #region
            CreateMap<UserTypeModel, UserTypeViewModel>().ReverseMap();
            CreateMap<UserTypeListModel, UserTypeListViewModel>().ReverseMap();
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
            CreateMap<OrganisationCentrewiseSmtpSettingModel, OrganisationCentrewiseSmtpSettingViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseEmailTemplateModel, OrganisationCentrewiseEmailTemplateViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseUserNameRegistrationModel, OrganisationCentrewiseUserNameRegistrationViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseSmsSettingModel, OrganisationCentrewiseSmsSettingViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseWhatsAppSettingModel, OrganisationCentrewiseWhatsAppSettingViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseJoiningCodeModel, OrganisationCentrewiseJoiningCodeViewModel>().ReverseMap();
            CreateMap<OrganisationCentrewiseSmtpSettingSendTestEmailModel, OrganisationCentrewiseSmtpSettingSendTestEmailViewModel>().ReverseMap();
            #endregion

            #region Employee            
            CreateMap<EmployeeCreateEditViewModel, GeneralPersonViewModel>().ReverseMap();
            CreateMap<EmployeeCreateEditViewModel, GeneralPersonModel>().ReverseMap();
            CreateMap<EmployeeMasterListModel, EmployeeMasterListViewModel>().ReverseMap();
            CreateMap<EmployeeMasterModel, EmployeeMasterViewModel>().ReverseMap();
            CreateMap<EmployeeServiceListModel, EmployeeServiceListViewModel>().ReverseMap();
            CreateMap<EmployeeServiceModel, EmployeeServiceViewModel>().ReverseMap();
            CreateMap<EmployeeCreateEditViewModel, EmployeeMasterModel>().ReverseMap();
            CreateMap<GeneralPersonAttendanceDetailsListViewModel, GeneralPersonAttendanceDetailsListModel>().ReverseMap();
            CreateMap<GeneralPersonAttendanceDetailsViewModel, GeneralPersonAttendanceDetailsModel>().ReverseMap();
            #endregion

            #region Person
            CreateMap<GeneralPersonFollowUpListViewModel, GeneralPersonFollowUpListModel>().ReverseMap();
            CreateMap<GeneralPersonFollowUpViewModel, GeneralPersonFollowUpModel>().ReverseMap();
            #endregion

            #region HMS
            CreateMap<HospitalDoctorsModel, HospitalDoctorsViewModel>().ReverseMap();
            CreateMap<HospitalDoctorsListModel, HospitalDoctorsListViewModel>().ReverseMap();
            CreateMap<HospitalDoctorAllocatedOPDRoomModel, HospitalDoctorAllocatedOPDRoomViewModel>().ReverseMap();
            CreateMap<HospitalDoctorAllocatedOPDRoomListModel, HospitalDoctorAllocatedOPDRoomListViewModel>().ReverseMap();
            CreateMap<HospitalDoctorOPDScheduleListModel, HospitalDoctorOPDScheduleListViewModel>().ReverseMap();
            CreateMap<HospitalDoctorLeaveScheduleModel, HospitalDoctorLeaveScheduleViewModel>().ReverseMap();
            CreateMap<HospitalDoctorVisitingChargesModel, HospitalDoctorVisitingChargesViewModel>().ReverseMap();
            CreateMap<HospitalDoctorLeaveScheduleListModel, HospitalDoctorLeaveScheduleListViewModel>().ReverseMap();
            CreateMap<HospitalDoctorVisitingChargesListModel, HospitalDoctorVisitingChargesListViewModel>().ReverseMap();

            CreateMap<HospitalPatientRegistrationCreateEditViewModel, GeneralPersonViewModel>().ReverseMap();
            CreateMap<HospitalPatientRegistrationCreateEditViewModel, GeneralPersonModel>().ReverseMap();
            CreateMap<HospitalPatientRegistrationListModel, HospitalPatientRegistrationListViewModel>().ReverseMap();
            CreateMap<HospitalPatientRegistrationModel, HospitalPatientRegistrationViewModel>().ReverseMap();
            CreateMap<HospitalRegistrationFeeListModel, HospitalRegistrationFeeListViewModel>().ReverseMap();
            CreateMap<HospitalRegistrationFeeModel, HospitalRegistrationFeeViewModel>().ReverseMap();
            CreateMap<HospitalDoctorOPDScheduleListModel, HospitalDoctorOPDScheduleListViewModel>().ReverseMap();
            CreateMap<HospitalDoctorOPDScheduleModel, HospitalDoctorOPDScheduleViewModel>().ReverseMap();
            CreateMap<HospitalPatientTypeListModel, HospitalPatientTypeListViewModel>().ReverseMap();
            CreateMap<HospitalPatientTypeModel, HospitalPatientTypeViewModel>().ReverseMap();
            CreateMap<HospitalPatientAppointmentModel, HospitalPatientAppointmentViewModel>().ReverseMap();
            CreateMap<HospitalPatientAppointmentListModel, HospitalPatientAppointmentListViewModel>().ReverseMap();
            CreateMap<HospitalPatientAppointmentPurposeModel, HospitalPatientAppointmentPurposeViewModel>().ReverseMap();
            CreateMap<HospitalPatientAppointmentPurposeListModel, HospitalPatientAppointmentPurposeListViewModel>().ReverseMap();

            CreateMap<HospitalPathologyTestGroupModel, HospitalPathologyTestGroupViewModel>().ReverseMap();
            CreateMap<HospitalPathologyTestGroupListModel, HospitalPathologyTestGroupListViewModel>().ReverseMap();
            CreateMap<HospitalPathologyTestModel, HospitalPathologyTestViewModel>().ReverseMap();
            CreateMap<HospitalPathologyTestListModel, HospitalPathologyTestListViewModel>().ReverseMap();
            CreateMap<HospitalPathologyTestPricesModel, HospitalPathologyTestPricesViewModel>().ReverseMap();
            CreateMap<HospitalPathologyTestPricesListModel, HospitalPathologyTestPricesListViewModel>().ReverseMap();
            #endregion

            #region Inventory
            CreateMap<InventoryCategoryModel, InventoryCategoryViewModel>().ReverseMap();
            CreateMap<InventoryItemStorageDimensionModel, InventoryItemStorageDimensionViewModel>().ReverseMap();
            CreateMap<InventoryCategoryListModel, InventoryCategoryListViewModel>().ReverseMap();
            CreateMap<InventoryItemModelGroupModel, InventoryItemModelGroupViewModel>().ReverseMap();
            CreateMap<InventoryItemModelGroupListModel, InventoryItemModelGroupListViewModel>().ReverseMap();

            CreateMap<InventoryProductDimensionGroupModel, InventoryProductDimensionGroupViewModel>().ReverseMap();
            CreateMap<InventoryProductDimensionGroupListModel, InventoryProductDimensionGroupListViewModel>().ReverseMap();
            CreateMap<InventoryItemTrackingDimensionModel, InventoryItemTrackingDimensionViewModel>().ReverseMap();
            CreateMap<InventoryProductDimensionModel, InventoryProductDimensionViewModel>().ReverseMap();
            CreateMap<InventoryProductDimensionListModel, InventoryProductDimensionListViewModel>().ReverseMap();
            CreateMap<InventoryItemTrackingDimensionListModel, InventoryItemTrackingDimensionListViewModel>().ReverseMap();
            CreateMap<InventoryItemGroupListModel, InventoryItemGroupListViewModel>().ReverseMap();
            CreateMap<InventoryItemGroupModel, InventoryItemGroupViewModel>().ReverseMap();
            CreateMap<InventoryUoMMasterModel, InventoryUoMMasterViewModel>().ReverseMap();
            CreateMap<InventoryUoMMasterListModel, InventoryUoMMasterListViewModel>().ReverseMap();
            CreateMap<InventoryStorageDimensionGroupModel, InventoryStorageDimensionGroupViewModel>().ReverseMap();
            CreateMap<InventoryStorageDimensionGroupListModel, InventoryStorageDimensionGroupListViewModel>().ReverseMap();
            CreateMap<InventoryItemTrackingDimensionGroupModel, InventoryItemTrackingDimensionGroupViewModel>().ReverseMap();
            CreateMap<InventoryItemTrackingDimensionGroupListModel, InventoryItemTrackingDimensionGroupListViewModel>().ReverseMap();
            CreateMap<InventoryGeneralItemMasterModel, InventoryGeneralItemMasterViewModel>().ReverseMap();
            CreateMap<InventoryGeneralItemMasterListModel, InventoryGeneralItemMasterListViewModel>().ReverseMap();
            CreateMap<InventoryCategoryTypeModel, InventoryCategoryTypeViewModel>().ReverseMap();
            CreateMap<InventoryCategoryTypeListModel, InventoryCategoryTypeListViewModel>().ReverseMap();
            #endregion

            #region Password
            CreateMap<ChangePasswordModel, ChangePasswordViewModel>().ReverseMap();
            CreateMap<ResetPasswordModel, ResetPasswordViewModel>().ReverseMap();
            CreateMap<ResetPasswordSendLinkModel, ResetPasswordViewModel>().ReverseMap();
            #endregion

            #region Dashboard
            CreateMap<DashboardModel, DashboardViewModel>().ReverseMap();
            #endregion

            #region Media Manager
            CreateMap<MediaSettingMasterModel, MediaSettingMasterViewModel>().ReverseMap();
            CreateMap<MediaSettingMasterListModel, MediaSettingMasterListViewModel>().ReverseMap();
            CreateMap<MediaManagerFolderModel, MediaManagerFolderListViewModel>().ReverseMap();
            CreateMap<FolderListModel, FolderListViewModel>().ReverseMap();
            #endregion

            #region Gazette
            CreateMap<GazetteChaptersModel, GazetteChaptersViewModel>().ReverseMap();
            CreateMap<GazetteChaptersListModel, GazetteChaptersListViewModel>().ReverseMap();
            CreateMap<GazetteChaptersPageDetailModel, GazetteChaptersPageDetailViewModel>().ReverseMap();
            CreateMap<GazetteChaptersPageDetailListModel, GazetteChaptersPageDetailListViewModel>().ReverseMap();
            #endregion

            #region LogMessage 
            CreateMap<LogMessageModel, LogMessageViewModel>().ReverseMap();
            CreateMap<LogMessageListModel, LogMessageListViewModel>().ReverseMap();
            #endregion

            #region Payment
            CreateMap<PaymentGatewaysModel, PaymentGatewaysViewModel>().ReverseMap();
            CreateMap<PaymentGatewaysListModel, PaymentGatewaysListViewModel>().ReverseMap();
            CreateMap<PaymentGatewayDetailsModel, PaymentGatewayDetailsViewModel>().ReverseMap();
            CreateMap<PaymentGatewayDetailsListModel, PaymentGatewayDetailsListViewModel>().ReverseMap();
            #endregion
        }
    }
}
