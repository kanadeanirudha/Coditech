using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Coditech.Admin.Helpers
{
    public static partial class CoditechDropdownHelper
    {
        public static List<UserAccessibleCentreModel> AccessibleCentreList()
        {
            return SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.AccessibleCentreList;
        }
        public static List<UserBalanceSheetModel> BindAccountBalanceSheetIdByCentreCodeList()
        {
            return SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.BalanceSheetList;
        }

        public static DropdownViewModel GeneralDropdownList(DropdownViewModel dropdownViewModel)
        {
            List<SelectListItem> dropdownList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(dropdownViewModel.GroupCode))
            {
                GetGeneralEnumaratorList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.City.ToString()))
            {
                GetCityList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Country.ToString()))
            {
                GetCountryList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Nationality.ToString()))
            {
                GetNationalityList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccessibleCentre.ToString()))
            {
                GetAccessibleCentreList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseDepartment.ToString()))
            {
                GetCentrewiseDepartmentList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Designation.ToString()))
            {
                GetDesignationList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Department.ToString()))
            {
                GetDepartmentList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Organisation.ToString()))
            {
                GetOrganisationList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.RegionalOffice.ToString()))
            {
                GetRegionalOfficeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.TaxGroup.ToString()))
            {
                GetTaxGroupList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Region.ToString()))
            {
                GetRegionList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.District.ToString()))
            {
                GetDistrictList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.ModuleList.ToString()))
            {
                GetModuleList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.MenuList.ToString()))
            {
                GetMenuList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.MaritalStatus.ToString()))
            {
                MaritalStatusList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.ReportType.ToString()))
            {
                ReportTypeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.BloodGroups.ToString()))
            {
                GetBloodGroupsList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Occupation.ToString()))
            {
                GetOccupationList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.MeasurementUnit.ToString()))
            {
                GetMeasurementUnitList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.PaymentGateway.ToString()))
            {
                GetPaymentGatewaysList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.FinancialYear.ToString()))
            {
                GetFinancialYearList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseBuilding.ToString()))
            {
                GetCentrewiseBuildingList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.UnAssociatedEmployeeList.ToString()))
            {
                GetUnAssociatedEmployeeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseBuildingRooms.ToString()))
            {
                GetCentrewiseBuildingRoomList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.GymMembershipPlan.ToString()))
            {
                GetGymMembershipPlanList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryGeneralServiecs.ToString()))
            {
                GetGeneralServicesList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AllCities.ToString()))
            {
                GetAllCityList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryCategory.ToString()))
            {
                GetInventoryCategoryList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.EmailTemplate.ToString()))
            {
                GetEmailTemplateCodeList(dropdownViewModel, dropdownList, "email");
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.SMSTemplate.ToString()))
            {
                GetEmailTemplateCodeList(dropdownViewModel, dropdownList, "sms");
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.WhatsAppTemplate.ToString()))
            {
                GetEmailTemplateCodeList(dropdownViewModel, dropdownList, "whatsapp");
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.HospitalDoctorsList.ToString()))
            {
                GetHospitalDoctorsList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryProductDimensionGroup.ToString()))
            {
                GetInventoryProductDimensionGroupList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryStorageDimensionGroup.ToString()))
            {
                GetInventoryStorageDimensionGroupList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryItemTrackingDimensionGroup.ToString()))
            {
                GetInventoryItemTrackingDimensionGroupList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryItemGroup.ToString()))
            {
                GetInventoryItemGroupList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.InventoryUomMaster.ToString()))
            {
                GetInventoryUomMasterList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.SMSProvider.ToString()))
            {
                GetSMSProviderList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.HospitalPatientType.ToString()))
            {
                GetHospitalPatientTypeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.WhatsAppProvider.ToString()))
            {
                GetWhatsAppProviderList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.HospitalPathologyTestGroup.ToString()))
            {
                GetHospitalPathologyTestGroupList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.HospitalDoctorsListBySpecialization.ToString()))
            {
                GetDoctorsByCentreCodeAndSpecialization(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.HospitalPatientAppointmentPurpose.ToString()))
            {
                GetHospitalPatientAppointmentPurposeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseHospitalPatientsList.ToString()))
            {
                GetHospitalPatientsListByCentreCode(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.TimeSlotByDoctorsListAndAppointmentDate.ToString()))
            {
                RequestedTimeSlotList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CallingCode.ToString()))
            {
                GetCountryCallingCode(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.HospitalPathologyTestGroupParent.ToString()))
            {
                GetHospitalPathologyTestGroupParentList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.PathologyTestNameByPathologyPriceCategory.ToString()))
            {
                GetPathologyTestNameByPathologyPriceCategory(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.UnAssociatedTrainerList.ToString()))
            {
                GetUnAssociatedTrainerList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccSetupBalanceSheetType.ToString()))
            {
                GetAccSetupBalanceSheetTypeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.UnAssociatedTrainerEmployeeList.ToString()))
            {
                GetUnAssociatedTrainerEmployeeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccSetupBalanceSheet.ToString()))
            {
                GetAccSetupBalanceSheet(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.SchedulerFrequency.ToString()))
            {
                GetBatchSchedulerList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.SchedulerWeeks.ToString()))
            {
                GetBatchSchedulerWeeksList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccSetupTransactionType.ToString()))
            {
                GetAccSetupTransactionType(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseAccountBalanceSheet.ToString()))
            {
                GetBindAccountBalanceSheetIdByCentreCodeList(dropdownViewModel, dropdownList);
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.DashboardDaysDropDown.ToString()))
            {
                DashboardDaysDropDownList(dropdownViewModel, dropdownList);
            }
            dropdownViewModel.DropdownList = dropdownList;
            return dropdownViewModel;
        }

        private static void GetSMSProviderList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralSmsProviderListResponse response = new GeneralSmsProviderClient().List(null, null, null, 1, int.MaxValue);
            GeneralSmsProviderListModel list = new GeneralSmsProviderListModel() { GeneralSmsProviderList = response.GeneralSmsProviderList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select SMS Provider-------", Value = "" });
            foreach (var item in list?.GeneralSmsProviderList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.ProviderName,
                    Value = item.GeneralSmsProviderId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralSmsProviderId)
                });
            }
        }
        private static void GetMeasurementUnitList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralMeasurementUnitListResponse response = new GeneralMeasurementUnitClient().List(null, null, null, 1, int.MaxValue);
            GeneralMeasurementUnitListModel list = new GeneralMeasurementUnitListModel() { GeneralMeasurementUnitList = response.GeneralMeasurementUnitList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select Measurement Unit-------" });
            foreach (var item in list?.GeneralMeasurementUnitList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.MeasurementUnitDisplayName,
                    Value = item.GeneralMeasurementUnitMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralMeasurementUnitMasterId)
                });
            }
        }
        private static void GetPaymentGatewaysList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            PaymentGatewaysListResponse response = new PaymentGatewaysClient().List(null, null, null, 1, int.MaxValue);

            PaymentGatewaysListModel list = new PaymentGatewaysListModel() { PaymentGatewaysList = response.PaymentGatewaysList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select Payment Gateways-------" });
            foreach (var item in list?.PaymentGatewaysList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.PaymentName,
                    Value = item.PaymentGatewayId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.PaymentGatewayId)
                });
            }
        }

        private static void GetOccupationList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralOccupationListResponse response = new GeneralOccupationClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });

            GeneralOccupationListModel list = new GeneralOccupationListModel { GeneralOccupationList = response.GeneralOccupationList };
            foreach (var item in list.GeneralOccupationList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.OccupationName,
                    Value = Convert.ToString(item.GeneralOccupationMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralOccupationMasterId)
                });
            }
        }

        private static void GetBloodGroupsList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Value = "NA", Text = GeneralResources.SelectLabel });
            dropdownList.Add(new SelectListItem()
            {
                Text = "A+",
                Value = "A+",
                Selected = "A+" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "A-",
                Value = "A-",
                Selected = "A-" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "B+",
                Value = "B+",
                Selected = "B+" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "B-",
                Value = "B-",
                Selected = "B-" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "AB+",
                Value = "AB+",
                Selected = "AB+" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "AB-",
                Value = "AB-",
                Selected = "AB-" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "O+",
                Value = "O+",
                Selected = "O+" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "O-",
                Value = "O-",
                Selected = "O-" == dropdownViewModel.DropdownSelectedValue
            });
        }

        private static void MaritalStatusList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
            dropdownList.Add(new SelectListItem()
            {
                Text = "Single",
                Value = "Single",
                Selected = "Single" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "Married",
                Value = "Married",
                Selected = "Married" == dropdownViewModel.DropdownSelectedValue
            });
        }

        private static void ReportTypeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem()
            {
                Text = "xls",
                Value = "xls",
                Selected = "xls" == dropdownViewModel.DropdownSelectedValue
            });
            dropdownList.Add(new SelectListItem()
            {
                Text = "pdf",
                Value = "pdf",
                Selected = "pdf" == dropdownViewModel.DropdownSelectedValue
            });
        }
        private static void GetMenuList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select-------", Value = "" });
            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                UserMainMenuListResponse response = new UserClient().GetActiveMenuList(dropdownViewModel.Parameter);

                foreach (var item in response?.MenuList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.MenuName,
                        Value = Convert.ToString(item.MenuCode),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.MenuCode)
                    });
                }
            }
        }

        private static void GetModuleList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            UserModuleListResponse response = new UserClient().GetActiveModuleList();
            dropdownList.Add(new SelectListItem() { Text = "-------Select-------", Value = "" });
            foreach (var item in response?.ModuleList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.ModuleName,
                    Value = Convert.ToString(item.ModuleCode),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.ModuleCode)
                });
            }
        }

        private static void GetRegionList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralRegionListModel list = new GeneralRegionListModel();
            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                GeneralRegionListResponse response = new GeneralRegionClient().GetRegionByCountryWise(Convert.ToInt16(dropdownViewModel.Parameter));
                list = new GeneralRegionListModel { GeneralRegionList = response?.GeneralRegionList };
            }
            dropdownList.Add(new SelectListItem() { Text = "-------- Select Region --------" });
            foreach (var item in list?.GeneralRegionList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.RegionName,
                    Value = item.GeneralRegionMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralRegionMasterId)
                });
            }
        }

        private static void GetDistrictList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralDistrictListModel list = new GeneralDistrictListModel();
            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                GeneralDistrictListResponse response = new GeneralDistrictClient().GetDistrictByRegionWise(Convert.ToInt16(dropdownViewModel.Parameter));
                list = new GeneralDistrictListModel { GeneralDistrictList = response?.GeneralDistrictList };
            }
            dropdownList.Add(new SelectListItem() { Text = "-------- Select District --------" });
            foreach (var item in list?.GeneralDistrictList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.DistrictName,
                    Value = item.GeneralDistrictMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralDistrictMasterId)
                });
            }
        }


        private static void GetTaxGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralTaxGroupListResponse response = new GeneralTaxGroupClient().List(null, null, null, 1, int.MaxValue);
            GeneralTaxGroupMasterListModel list = new GeneralTaxGroupMasterListModel() { GeneralTaxGroupMasterList = response.GeneralTaxGroupMasterList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select-------" });
            foreach (var item in list?.GeneralTaxGroupMasterList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.TaxGroupName, " (", item.TaxGroupRate, "%)"),
                    Value = Convert.ToString(item.GeneralTaxGroupMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralTaxGroupMasterId)
                });
            }
        }

        private static void GetFinancialYearList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralFinancialYearListResponse response = new GeneralFinancialYearClient().List(null, null, null, 1, int.MaxValue);
            GeneralFinancialYearListModel list = new GeneralFinancialYearListModel() { GeneralFinancialYearList = response.GeneralFinancialYearList };
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });

            foreach (var item in list?.GeneralFinancialYearList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.FromDate.ToCoditechDateFormat(), " To ", item.ToDate.ToCoditechDateFormat()),
                    Value = Convert.ToString(item.GeneralFinancialYearId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralFinancialYearId)
                });
            }
        }

        private static void GetCentrewiseBuildingList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });


            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                FilterCollection filters = new FilterCollection();
                filters.Add(FilterKeys.SelectedCentreCode, ProcedureFilterOperators.Equals, dropdownViewModel.Parameter);
                OrganisationCentrewiseBuildingListResponse response = new OrganisationCentrewiseBuildingClient().List(null, filters, null, 1, int.MaxValue);
                OrganisationCentrewiseBuildingListModel list = new OrganisationCentrewiseBuildingListModel() { OrganisationCentrewiseBuildingList = response.OrganisationCentrewiseBuildingList };
                foreach (var item in list?.OrganisationCentrewiseBuildingList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.BuildingName,
                        Value = item.OrganisationCentrewiseBuildingMasterId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationCentrewiseBuildingMasterId)
                    });
                }
            }
        }

        private static void GetUnAssociatedEmployeeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Employee-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter.Split("~")[0];
                short selectedDepartmentId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[1]);
                HospitalDoctorsListResponse response = new HospitalDoctorsClient().List(selectedCentreCode, selectedDepartmentId, false, null, null, null, 1, int.MaxValue);
                HospitalDoctorsListModel list = new HospitalDoctorsListModel() { HospitalDoctorsList = response.HospitalDoctorsList };
                foreach (var item in list?.HospitalDoctorsList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.FirstName} {item.LastName}",
                        Value = item.EmployeeId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.EmployeeId)
                    });
                }
            }
        }

        private static void GetUnAssociatedTrainerEmployeeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Employee-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter.Split("~")[0];
                short selectedDepartmentId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[1]);
                GeneralTrainerListResponse response = new GeneralTrainerClient().List(selectedCentreCode, selectedDepartmentId, false, null, null, null, 1, int.MaxValue);
                GeneralTrainerListModel list = new GeneralTrainerListModel() { GeneralTrainerList = response.GeneralTrainerList };
                foreach (var item in list?.GeneralTrainerList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.FirstName} {item.LastName}",
                        Value = item.EmployeeId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.EmployeeId)
                    });
                }
            }
        }
        private static void GetCentrewiseBuildingRoomList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Room-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                FilterCollection filters = new FilterCollection();
                filters.Add(FilterKeys.OrganisationCentrewiseBuildingMasterId, ProcedureFilterOperators.Equals, dropdownViewModel.Parameter);
                OrganisationCentrewiseBuildingRoomsListResponse response = new OrganisationCentrewiseBuildingRoomsClient().List(null, filters, null, 1, int.MaxValue);
                OrganisationCentrewiseBuildingRoomsListModel list = new OrganisationCentrewiseBuildingRoomsListModel() { OrganisationCentrewiseBuildingRoomsList = response.OrganisationCentrewiseBuildingRoomsList };
                foreach (var item in list?.OrganisationCentrewiseBuildingRoomsList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.RoomName,
                        Value = item.OrganisationCentrewiseBuildingRoomId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationCentrewiseBuildingRoomId)
                    });
                }
            }
        }

        private static void GetGymMembershipPlanList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Membership Plan-------", Value = "" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                FilterCollection filters = new FilterCollection();
                filters.Add(FilterKeys.IsActive, ProcedureFilterOperators.Equals, "1");
                GymMembershipPlanListResponse response = new GymMembershipPlanClient().List(dropdownViewModel.Parameter, null, filters, null, 1, int.MaxValue);
                GymMembershipPlanListModel list = new GymMembershipPlanListModel() { GymMembershipPlanList = response.GymMembershipPlanList };
                foreach (var item in list?.GymMembershipPlanList)
                {
                    string planDuration = item.PlanDurationType.ToLower() == "duration" ? $"{item.PlanDurationInMonth} Month {item.PlanDurationInDays} Days" : $"{item.PlanDurationInSession} Session";
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.MembershipPlanName}-{item.PlanType}-{planDuration}",
                        Value = $"{item.GymMembershipPlanId.ToString()}~{item.PlanDurationType}~{item.MaxCost}~{(item.MaxCost - item.MinCost)}",
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GymMembershipPlanId)
                    });
                }
            }
        }

        private static void GetRegionalOfficeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem()
            {
                Text = "Centre",
                Value = "CO",
                Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(dropdownViewModel.Parameter)
            });

            dropdownList.Add(new SelectListItem()
            {
                Text = "Regional Office",
                Value = "RO",
                Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(dropdownViewModel.Parameter)
            });
        }

        private static void GetOrganisationList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            OrganisationResponse item = new OrganisationClient().GetOrganisation();
            dropdownList.Add(new SelectListItem()
            {
                Text = item.OrganisationModel.OrganisationName,
                Value = item.OrganisationModel.OrganisationMasterId.ToString(),
                Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationModel.OrganisationMasterId)
            });
        }

        private static void GetDepartmentList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralDepartmentListResponse response = new GeneralDepartmentClient().List(null, null, null, 1, int.MaxValue);
            GeneralDepartmentListModel list = new GeneralDepartmentListModel() { GeneralDepartmentList = response.GeneralDepartmentList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select Department-------" });
            foreach (var item in list?.GeneralDepartmentList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.DepartmentName,
                    Value = item.GeneralDepartmentMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralDepartmentMasterId)
                });
            }
        }

        private static void GetDesignationList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralDesignationListResponse response = new GeneralDesignationClient().List(null, null, null, 1, int.MaxValue);
            GeneralDesignationListModel list = new GeneralDesignationListModel() { GeneralDesignationList = response.GeneralDesignationList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select Designation-------" });
            foreach (var item in list.GeneralDesignationList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.EmployeeDesignationMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.EmployeeDesignationMasterId)
                });
            }
        }

        private static void GetCentrewiseDepartmentList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (string.IsNullOrEmpty(dropdownViewModel.Parameter) && AccessibleCentreList()?.Count == 1)
            {
                dropdownViewModel.Parameter = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession).SelectedCentreCode;
            }
            GeneralDepartmentListModel list = new GeneralDepartmentListModel();
            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string centreCode = SpiltCentreCode(dropdownViewModel.Parameter);
                GeneralDepartmentListResponse response = new GeneralDepartmentClient().GetDepartmentsByCentreCode(centreCode);
                list = new GeneralDepartmentListModel { GeneralDepartmentList = response?.GeneralDepartmentList };
            }
            dropdownList.Add(new SelectListItem() { Text = "-------Select Department-------", Value = "" });
            foreach (var item in list?.GeneralDepartmentList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.DepartmentName,
                    Value = item.GeneralDepartmentMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralDepartmentMasterId)
                });
            }
        }

        private static void GetAccessibleCentreList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            List<UserAccessibleCentreModel> accessibleCentreList = AccessibleCentreList();
            if (accessibleCentreList?.Count == 1)
                dropdownViewModel.DropdownSelectedValue = accessibleCentreList.FirstOrDefault().CentreCode;
            else
                dropdownList.Add(new SelectListItem() { Text = "-------Select Centre-------", Value = "" });

            foreach (var item in accessibleCentreList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.CentreName,
                    Value = item.CentreCode,
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.CentreCode)
                });
            }
        }

        private static void GetNationalityList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralNationalityListResponse response = new GeneralNationalityClient().List(null, null, null, 1, int.MaxValue);
            if (response?.GeneralNationalityList?.Count != 1)
                dropdownList.Add(new SelectListItem() { Text = "-------Select -------" });

            GeneralNationalityListModel list = new GeneralNationalityListModel { GeneralNationalityList = response?.GeneralNationalityList };
            foreach (var item in list.GeneralNationalityList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.Description,
                    Value = Convert.ToString(item.GeneralNationalityMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralNationalityMasterId)
                });
            }
        }

        private static void GetCountryList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralCountryListResponse response = new GeneralCountryClient().List(null, null, null, 1, int.MaxValue);
            //if (response?.GeneralCountryList?.Count != 1)
            dropdownList.Add(new SelectListItem() { Text = "-------Select Country-------" });

            GeneralCountryListModel list = new GeneralCountryListModel { GeneralCountryList = response.GeneralCountryList };
            foreach (var item in list.GeneralCountryList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.CountryName, " (", item.CountryCode, ")"),
                    Value = Convert.ToString(item.GeneralCountryMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralCountryMasterId)
                });
            }
        }

        private static void GetCountryCallingCode(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralCountryListResponse response = new GeneralCountryClient().List(null, null, null, 1, int.MaxValue);
            dropdownList.Add(new SelectListItem() { Value = "", Text = "--Select--" });

            GeneralCountryListModel list = new GeneralCountryListModel { GeneralCountryList = response.GeneralCountryList };
            foreach (var item in list.GeneralCountryList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.CallingCode,
                    Value = Convert.ToString(item.CallingCode),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.CallingCode)
                });
            }
        }

        private static void GetCityList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            short regionId = Convert.ToInt16(dropdownViewModel.Parameter);
            GeneralCityListResponse response = new GeneralCityClient().GetCityByRegionWise(Convert.ToInt16(dropdownViewModel.Parameter));
            dropdownList.Add(new SelectListItem() { Text = "-------Select City-------" });
            GeneralCityListModel list = new GeneralCityListModel { GeneralCityList = response?.GeneralCityList };
            foreach (var item in list.GeneralCityList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.CityName, " (", item.RegionName, ")"),
                    Value = Convert.ToString(item.GeneralCityMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralCityMasterId)
                });
            }
        }

        private static void GetGeneralEnumaratorList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            List<GeneralEnumaratorModel> generalEnumaratorList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralEnumaratorList;
            if (generalEnumaratorList == null)
            {
                generalEnumaratorList = new GeneralCommonClient().GetDropdownListByCode(dropdownViewModel.GroupCode)?.GeneralEnumaratorList;
            }
            if (dropdownViewModel.AddSelectItem)
            {
                if (dropdownViewModel.IsRequired)
                {
                    if (dropdownViewModel.IsStringProperty)
                        dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
                    else
                        dropdownList.Add(new SelectListItem() { Text = GeneralResources.SelectLabel });
                }
                else
                    dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });
            }
            if (generalEnumaratorList != null)
            {
                foreach (var item in generalEnumaratorList?.Where(x => x.EnumGroupCode == dropdownViewModel.GroupCode)?.OrderBy(y => y.SequenceNumber))
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.EnumDisplayText,
                        Value = dropdownViewModel.IsTextValueSame ? item.EnumName : Convert.ToString(item.GeneralEnumaratorId),
                        Selected = dropdownViewModel.IsTextValueSame ? dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.EnumName) : dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralEnumaratorId)
                    });
                }
            }
        }

        private static void GetGeneralServicesList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Service-------", Value = "" });

            InventoryGeneralItemMasterListResponse response = new InventoryGeneralItemMasterClient().GetGeneralServicesList(dropdownViewModel.Parameter);
            InventoryGeneralItemMasterListModel list = new InventoryGeneralItemMasterListModel() { InventoryGeneralItemMasterList = response.InventoryGeneralItemMasterList };
            foreach (var item in list?.InventoryGeneralItemMasterList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = $"{item.ItemName}({item.HSNSACCode})",
                    Value = item.InventoryGeneralItemLineId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryGeneralItemLineId)
                });
            }
        }
        private static void GetAllCityList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralCityListResponse response = new GeneralCityClient().GetAllCities();
            dropdownList.Add(new SelectListItem() { Text = "-------Select City-------" });
            GeneralCityListModel list = new GeneralCityListModel { GeneralCityList = response?.GeneralCityList };
            foreach (var item in list.GeneralCityList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.CityName,
                    Value = Convert.ToString(item.GeneralCityMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralCityMasterId)
                });
            }
        }

        private static string SpiltCentreCode(string centreCode)
        {
            centreCode = !string.IsNullOrEmpty(centreCode) && centreCode.Contains(":") ? centreCode.Split(':')[0] : centreCode;
            return centreCode;
        }

        private static void GetInventoryCategoryList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            InventoryCategoryListResponse response = new InventoryCategoryClient().List(null, null, null, 1, int.MaxValue);
            dropdownList.Add(new SelectListItem() { Text = "-------Select Category-------" });

            InventoryCategoryListModel list = new InventoryCategoryListModel { InventoryCategoryList = response.InventoryCategoryList };
            foreach (var item in list.InventoryCategoryList)
            {
                if (!string.IsNullOrEmpty(dropdownViewModel.Parameter) && Convert.ToInt16(dropdownViewModel.Parameter) > 0 && item.InventoryCategoryId == Convert.ToInt16(dropdownViewModel.Parameter))
                {
                    continue;
                }
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.CategoryName, " (", item.CategoryCode, ")"),
                    Value = Convert.ToString(item.InventoryCategoryId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryCategoryId)
                });
            }
        }
        private static void GetHospitalPathologyTestGroupParentList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            HospitalPathologyTestGroupListResponse response = new HospitalPathologyTestGroupClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });


            HospitalPathologyTestGroupListModel list = new HospitalPathologyTestGroupListModel { HospitalPathologyTestGroupList = response.HospitalPathologyTestGroupList };
            foreach (var item in list.HospitalPathologyTestGroupList)
            {
                if (!string.IsNullOrEmpty(dropdownViewModel.Parameter) && Convert.ToInt16(dropdownViewModel.Parameter) > 0 && item.HospitalPathologyTestGroupId == Convert.ToInt16(dropdownViewModel.Parameter))
                {
                    continue;
                }
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.PathologyTestGroupName,
                    Value = Convert.ToString(item.HospitalPathologyTestGroupId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalPathologyTestGroupId)
                });
            }
        }

        private static void GetEmailTemplateCodeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList, string templateType)
        {
            FilterCollection filters = new FilterCollection();
            if (templateType == "email")
            {
                filters.Add(FilterKeys.IsEmailTemplate, ProcedureFilterOperators.Is, "1");
            }
            else if (templateType == "sms")
            {
                filters.Add("IsSmsTemplate", ProcedureFilterOperators.Is, "1");
            }
            else if (templateType == "whatsapp")
            {
                filters.Add("IsWhatsAppTemplate", ProcedureFilterOperators.Is, "1");
            }
            GeneralEmailTemplateListResponse response = new GeneralEmailTemplateClient().List(null, filters, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Template-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select Template-------" });

            GeneralEmailTemplateListModel list = new GeneralEmailTemplateListModel { GeneralEmailTemplateList = response.GeneralEmailTemplateList };
            foreach (var item in list.GeneralEmailTemplateList?.Where(x => x.IsActive))
            {
                if (!string.IsNullOrEmpty(dropdownViewModel.Parameter) && Convert.ToInt16(dropdownViewModel.Parameter) > 0 && item.GeneralEmailTemplateId == Convert.ToInt16(dropdownViewModel.Parameter))
                {
                    continue;
                }
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.EmailTemplateName),
                    Value = Convert.ToString(item.EmailTemplateCode),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.EmailTemplateCode)
                });
            }
        }

        private static void GetInventoryProductDimensionGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            InventoryProductDimensionGroupListResponse response = new InventoryProductDimensionGroupClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            InventoryProductDimensionGroupListModel list = new InventoryProductDimensionGroupListModel { InventoryProductDimensionGroupList = response.InventoryProductDimensionGroupList };
            foreach (var item in list.InventoryProductDimensionGroupList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.ProductDimensionGroupName, " (", item.ProductDimensionGroupCode, ")"),
                    Value = Convert.ToString(item.InventoryProductDimensionGroupId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryProductDimensionGroupId)
                });
            }
        }

        private static void GetInventoryStorageDimensionGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            InventoryStorageDimensionGroupListResponse response = new InventoryStorageDimensionGroupClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            InventoryStorageDimensionGroupListModel list = new InventoryStorageDimensionGroupListModel { InventoryStorageDimensionGroupList = response.InventoryStorageDimensionGroupList };
            foreach (var item in list.InventoryStorageDimensionGroupList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.StorageDimensionGroupName, " (", item.StorageDimensionGroupCode, ")"),
                    Value = Convert.ToString(item.InventoryStorageDimensionGroupId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryStorageDimensionGroupId)
                });
            }
        }

        private static void GetInventoryItemTrackingDimensionGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            InventoryItemTrackingDimensionGroupListResponse response = new InventoryItemTrackingDimensionGroupClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            InventoryItemTrackingDimensionGroupListModel list = new InventoryItemTrackingDimensionGroupListModel { InventoryItemTrackingDimensionGroupList = response.InventoryItemTrackingDimensionGroupList };
            foreach (var item in list.InventoryItemTrackingDimensionGroupList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.ItemTrackingDimensionGroupName, " (", item.ItemTrackingDimensionGroupCode, ")"),
                    Value = Convert.ToString(item.InventoryItemTrackingDimensionGroupId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryItemTrackingDimensionGroupId)
                });
            }
        }

        private static void GetInventoryItemGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            InventoryItemGroupListResponse response = new InventoryItemGroupClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            InventoryItemGroupListModel list = new InventoryItemGroupListModel { InventoryItemGroupList = response.InventoryItemGroupList };
            foreach (var item in list.InventoryItemGroupList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.ItemGroupName, " (", item.ItemGroupCode, ")"),
                    Value = Convert.ToString(item.InventoryItemGroupId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryItemGroupId)
                });
            }
        }

        private static void GetInventoryUomMasterList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            InventoryUoMMasterListResponse response = new InventoryUoMMasterClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            InventoryUoMMasterListModel list = new InventoryUoMMasterListModel { InventoryUoMMasterList = response.InventoryUoMMasterList };
            foreach (var item in list.InventoryUoMMasterList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = $"{item.UomCode}({item.UomDescription}-{item.MeasurementUnitDisplayName})",
                    Value = Convert.ToString(item.InventoryUoMMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.InventoryUoMMasterId)
                });
            }
        }

        private static void GetHospitalDoctorsList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Doctors-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select Doctors-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter.Split("~")[0];
                short selectedDepartmentId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[1]);
                HospitalDoctorsListResponse response = new HospitalDoctorsClient().List(selectedCentreCode, selectedDepartmentId, true, null, null, null, 1, int.MaxValue);
                HospitalDoctorsListModel list = new HospitalDoctorsListModel() { HospitalDoctorsList = response.HospitalDoctorsList };
                foreach (var item in list?.HospitalDoctorsList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.FirstName} {item.LastName}",
                        Value = item.HospitalDoctorId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalDoctorId)
                    });
                }
            }
        }

        private static void GetHospitalPatientTypeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            HospitalPatientTypeListResponse response = new HospitalPatientTypeClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            HospitalPatientTypeListModel list = new HospitalPatientTypeListModel { HospitalPatientTypeList = response.HospitalPatientTypeList };
            foreach (var item in list?.HospitalPatientTypeList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = $"{item.PatientType}",
                    Value = Convert.ToString(item.HospitalPatientTypeId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalPatientTypeId)
                });
            }
        }

        private static void GetDoctorsByCentreCodeAndSpecialization(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Doctors-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select Doctors-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter.Split("~")[0];
                int medicalSpecializationEnumId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[1]);
                HospitalDoctorsListResponse response = new HospitalPatientAppointmentClient().GetDoctorsByCentreCodeAndSpecialization(selectedCentreCode, medicalSpecializationEnumId);
                HospitalDoctorsListModel list = new HospitalDoctorsListModel() { HospitalDoctorsList = response.HospitalDoctorsList };
                foreach (var item in list?.HospitalDoctorsList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.FirstName} {item.LastName}",
                        Value = item.HospitalDoctorId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalDoctorId)
                    });
                }
            }
        }

        private static void GetHospitalPatientAppointmentPurposeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            HospitalPatientAppointmentPurposeListResponse response = new HospitalPatientAppointmentPurposeClient().List(null, null, null, 1, int.MaxValue);
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Text = "-------Select-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select-------" });

            HospitalPatientAppointmentPurposeListModel list = new HospitalPatientAppointmentPurposeListModel { HospitalPatientAppointmentPurposeList = response.HospitalPatientAppointmentPurposeList };
            foreach (var item in list?.HospitalPatientAppointmentPurposeList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = $"{item.AppointmentPurpose}",
                    Value = Convert.ToString(item.HospitalPatientAppointmentPurposeId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalPatientAppointmentPurposeId)
                });
            }
        }

        private static void GetHospitalPathologyTestGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            HospitalPathologyTestGroupListResponse response = new HospitalPathologyTestGroupClient().List(null, null, null, 1, int.MaxValue);
            dropdownList.Add(new SelectListItem() { Text = "-------Select Pathology Test Group-------" });

            HospitalPathologyTestGroupListModel list = new HospitalPathologyTestGroupListModel { HospitalPathologyTestGroupList = response.HospitalPathologyTestGroupList };
            foreach (var item in list.HospitalPathologyTestGroupList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.PathologyTestGroupName,
                    Value = Convert.ToString(item.HospitalPathologyTestGroupId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalPathologyTestGroupId)
                });
            }
        }

        private static void GetPathologyTestNameByPathologyPriceCategory(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Pathology Test Name-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select Pathology Test Name-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter) && dropdownViewModel.Parameter != "0")
            {
                int hospitalPathologyPriceCategoryEnumId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[0]);
                FilterCollection filters = null;
                if (Convert.ToInt32(dropdownViewModel.DropdownSelectedValue) > 0)
                {
                    filters = new FilterCollection();
                    filters.Add("a.HospitalPathologyTestId", ProcedureFilterOperators.Equals, dropdownViewModel.DropdownSelectedValue);
                    hospitalPathologyPriceCategoryEnumId = 0;
                }

                HospitalPathologyTestPricesListResponse response = new HospitalPathologyTestPricesClient().List(hospitalPathologyPriceCategoryEnumId, null, filters, null, 1, int.MaxValue);
                HospitalPathologyTestPricesListModel list = new HospitalPathologyTestPricesListModel() { HospitalPathologyTestPricesList = response.HospitalPathologyTestPricesList };
                foreach (var item in list?.HospitalPathologyTestPricesList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.PathologyTestName,
                        Value = item.HospitalPathologyTestId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalPathologyTestId)
                    });
                }
            }
        }
        private static void GetWhatsAppProviderList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select WhatsApp Provider-------", Value = "" });
            dropdownList.Add(new SelectListItem()
            {
                Text = "Twilio",
                Value = "1",
                Selected = "1" == dropdownViewModel.DropdownSelectedValue
            });
        }

        private static void GetHospitalPatientsListByCentreCode(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Patients-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select Patients-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter;
                //short selectedDepartmentId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[1]);
                HospitalPatientRegistrationListResponse response = new HospitalPatientRegistrationClient().List(selectedCentreCode, null, null, null, 1, int.MaxValue);
                HospitalPatientRegistrationListModel list = new HospitalPatientRegistrationListModel() { HospitalPatientRegistrationList = response.HospitalPatientRegistrationList };
                foreach (var item in list?.HospitalPatientRegistrationList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.FirstName} {item.LastName}",
                        Value = item.HospitalPatientRegistrationId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.HospitalPatientRegistrationId)
                    });
                }
            }
        }

        private static void RequestedTimeSlotList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {

            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Time Slot-------" });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = "-------Select Time Slot-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                int hospitalDoctorId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[0]);
                DateTime appointmentDate = DateTime.Parse(dropdownViewModel.Parameter.Split("~")[1]);
                HospitalPatientTimeSlotListResponse response = new HospitalPatientAppointmentClient().GetTimeSlotByDoctorsAndAppointmentDate(hospitalDoctorId, appointmentDate);

                HospitalPatientTimeSlotListModel list = new HospitalPatientTimeSlotListModel() { TimeSlotList = response.TimeSlotList };
                foreach (var item in list?.TimeSlotList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = "10:00 AM-10:15 AM",
                        Value = "10:00",
                        Selected = "10:00" == dropdownViewModel.DropdownSelectedValue
                    });
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = "10:16 AM-10:30 AM",
                        Value = "10:15",
                        Selected = "10:15" == dropdownViewModel.DropdownSelectedValue
                    });
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = "10:31 AM-10:45 AM",
                        Value = "10:30",
                        Selected = "10:30" == dropdownViewModel.DropdownSelectedValue
                    });
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = "10:46 AM-11:00 AM",
                        Value = "10:45",
                        Selected = "10:45" == dropdownViewModel.DropdownSelectedValue
                    });
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = "11:00 AM-11:15 AM",
                        Value = "11:00",
                        Selected = "11:00" == dropdownViewModel.DropdownSelectedValue
                    });
                }
            }
        }

        private static void GetUnAssociatedTrainerList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Trainer-------" });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter.Split("~")[0];
                short selectedDepartmentId = Convert.ToInt16(dropdownViewModel.Parameter.Split("~")[1]);
                long entityId = Convert.ToInt64(dropdownViewModel.Parameter.Split("~")[2]);
                string userType = Convert.ToString(dropdownViewModel.Parameter.Split("~")[3]);
                bool isAssociated = Convert.ToBoolean(dropdownViewModel.Parameter.Split("~")[4]);
                GeneralTraineeAssociatedToTrainerListResponse response = new GeneralTrainerClient().GetAssociatedTrainerList(selectedCentreCode, selectedDepartmentId, isAssociated, entityId, userType, 0, null, null, null, 1, int.MaxValue);
                GeneralTraineeAssociatedToTrainerListModel list = new GeneralTraineeAssociatedToTrainerListModel() { AssociatedTrainerList = response.AssociatedTrainerList };
                foreach (var item in list?.AssociatedTrainerList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = $"{item.FirstName} {item.LastName}",
                        Value = item.GeneralTrainerMasterId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralTrainerMasterId)
                    });
                }
            }
        }

        private static void GetAccSetupBalanceSheetTypeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            AccSetupBalanceSheetTypeListResponse response = new AccSetupBalanceSheetTypeClient().List(null, null, null, 1, int.MaxValue);

            AccSetupBalanceSheetTypeListModel list = new AccSetupBalanceSheetTypeListModel() { AccSetupBalanceSheetTypeList = response.AccSetupBalanceSheetTypeList };
            //dropdownList.Add(new SelectListItem() { Text = "-------BalanceSheet Type-------" });
            foreach (var item in list?.AccSetupBalanceSheetTypeList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.AccBalsheetTypeDesc,
                    Value = item.AccSetupBalanceSheetTypeId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.AccSetupBalanceSheetTypeId)
                });
            }

            // Check if there are at least 2 items to swap
            if (dropdownList.Count > 1)
            {
                // Swap 0th and 1st position
                var temp = dropdownList[0];
                dropdownList[0] = dropdownList[1];
                dropdownList[1] = temp;
            }
        }

        private static void GetAccSetupBalanceSheet(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            if (dropdownViewModel.IsRequired)
                dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
            else
                dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });

            if (!string.IsNullOrEmpty(dropdownViewModel.Parameter))
            {
                string selectedCentreCode = dropdownViewModel.Parameter.Split("~")[0];
                byte accSetupBalanceSheetTypeId = Convert.ToByte(dropdownViewModel.Parameter.Split("~")[1]);
                AccSetupBalanceSheetListResponse response = new AccSetupBalanceSheetClient().List(selectedCentreCode, accSetupBalanceSheetTypeId, null, null, null, 1, int.MaxValue);
                AccSetupBalanceSheetListModel list = new AccSetupBalanceSheetListModel() { AccSetupBalanceSheetList = response.AccSetupBalanceSheetList };

                foreach (var item in list?.AccSetupBalanceSheetList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.AccBalancesheetHeadDesc,
                        Value = item.AccSetupBalanceSheetId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.AccSetupBalanceSheetId)
                    });
                }
            }
        }
        private static void GetBatchSchedulerList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            foreach (SchedulerFrequencyEnum frequency in Enum.GetValues(typeof(SchedulerFrequencyEnum)))
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = frequency.ToString(),
                    Value = frequency.ToString(),
                    Selected = frequency.ToString() == dropdownViewModel.DropdownSelectedValue
                });
            }
        }

        private static void GetBatchSchedulerWeeksList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Value = "NA", Text = GeneralResources.SelectLabel });

            //string[] selectedValues = dropdownViewModel.DropdownSelectedValue?.Split(',') ?? new string[] { };
            string[] selectedValues = !string.IsNullOrEmpty(dropdownViewModel.DropdownSelectedValue)
        ? dropdownViewModel.DropdownSelectedValue.Split(',')
        : new string[] { };


            foreach (var day in new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" })
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = day,
                    Value = day,
                    Selected = selectedValues.Contains(day)
                });
            }
        }

        private static void GetAccSetupTransactionType(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            AccSetupTransactionTypeListResponse response = new AccSetupTransactionTypeClient().List(null, null, null, 1, int.MaxValue);

            AccSetupTransactionTypeListModel list = new AccSetupTransactionTypeListModel() { AccSetupTransactionTypeList = response.AccSetupTransactionTypeList };

            dropdownList.Add(new SelectListItem() { Text = "-------Transaction Type-------" });
            foreach (var item in list?.AccSetupTransactionTypeList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.TransactionTypeCode,
                    Value = item.AccSetupTransactionTypeId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.AccSetupTransactionTypeId)
                });
            }
        }
        private static void GetBindAccountBalanceSheetIdByCentreCodeList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            List<UserBalanceSheetModel> BalanceSheetList = BindAccountBalanceSheetIdByCentreCodeList();
            dropdownList.Add(new SelectListItem() { Text = "-------Select Balancesheet-------", Value = "" });

            foreach (var item in BalanceSheetList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.AccBalancesheetHeadDesc,
                    Value = item.AccSetupBalanceSheetId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.AccSetupBalanceSheetId)
                });
            }
        }

        private static void DashboardDaysDropDownList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            foreach (string item in CoditechAdminSettings.DashboardDays.Split(','))
            {
                if (Convert.ToInt16(item) > 1)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Value = item,
                        Text = $"Last {item} Days",
                        Selected = item == dropdownViewModel.DropdownSelectedValue
                    });
                }
                else if (Convert.ToInt16(item) == 1)
                {
                    dropdownList.Add(new SelectListItem() { Value = item, Text = $"Yesterday", Selected = item == dropdownViewModel.DropdownSelectedValue });
                }
                else if (Convert.ToInt16(item) == 0)
                {
                    dropdownList.Add(new SelectListItem() { Value = item, Text = $"Today", Selected = item == dropdownViewModel.DropdownSelectedValue });
                }
            }
        }
    }
}
