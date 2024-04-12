using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Helpers
{
    public static class CoditechDropdownHelper
    {
        public static List<UserAccessibleCentreModel> AccessibleCentreList()
        {
            return SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.AccessibleCentreList;
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
                HetMaritalStatusList(dropdownViewModel, dropdownList);
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
            dropdownViewModel.DropdownList = dropdownList;
            return dropdownViewModel;
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

        private static void HetMaritalStatusList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
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

        private static void GetMenuList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralTaxGroupListResponse response = new GeneralTaxGroupClient().List(null, null, null, 1, int.MaxValue);
            GeneralTaxGroupMasterListModel list = new GeneralTaxGroupMasterListModel() { GeneralTaxGroupMasterList = response.GeneralTaxGroupMasterList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select Tax Group-------" });
            foreach (var item in list?.GeneralTaxGroupMasterList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.TaxGroupName, " (", item.GeneralTaxMasterIds, ")"),
                    Value = Convert.ToString(item.GeneralTaxGroupMasterId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralTaxGroupMasterId)
                });
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
            dropdownList.Add(new SelectListItem() { Text = "-------- Select State --------" });
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

        private static void GetTaxGroupList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            GeneralTaxGroupListResponse response = new GeneralTaxGroupClient().List(null, null, null, 1, int.MaxValue);
            GeneralTaxGroupMasterListModel list = new GeneralTaxGroupMasterListModel() { GeneralTaxGroupMasterList = response.GeneralTaxGroupMasterList };
            dropdownList.Add(new SelectListItem() { Text = "-------Select Tax Group-------" });
            foreach (var item in list?.GeneralTaxGroupMasterList)
            {
                dropdownList.Add(new SelectListItem()
                {
                    Text = string.Concat(item.TaxGroupName, " (", item.GeneralTaxMasterIds, ")"),
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
                    Text = string.Concat(item.FromDate.ToShortDateString(), " To ", item.ToDate.ToShortDateString()),
                    Value = Convert.ToString(item.GeneralFinancialYearId),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.GeneralFinancialYearId)
                });
            }
        }

        private static void GetCentrewiseBuildingList(DropdownViewModel dropdownViewModel, List<SelectListItem> dropdownList)
        {
            dropdownList.Add(new SelectListItem() { Text = "-------Select Building-------" });

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
                Text = "Head Office",
                Value = "HO",
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
            //if (AccessibleCentreList()?.Count == 1 && string.IsNullOrEmpty(dropdownViewModel.Parameter))
            //{
            //    dropdownViewModel.Parameter = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession).SelectedCentreCode;
            //}
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
            //if (accessibleCentreList?.Count != 1)
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
            if (dropdownViewModel.AddSelectItem)
            {
                if (dropdownViewModel.IsRequired)
                    dropdownList.Add(new SelectListItem() { Text = GeneralResources.SelectLabel });
                else
                    dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });
            }
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
                    Text =item.CityName,
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
    }
}
