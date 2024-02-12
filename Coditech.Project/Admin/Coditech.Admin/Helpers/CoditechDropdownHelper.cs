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
                List<GeneralEnumaratorModel> generalEnumaratorList = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralEnumaratorList;
                if (dropdownViewModel.IsRequired)
                    dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
                else
                    dropdownList.Add(new SelectListItem() { Value = "0", Text = GeneralResources.SelectLabel });

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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.City.ToString()))
            {
                short regionId = Convert.ToInt16(dropdownViewModel.Parameter);
                FilterCollection filters = null;
                if (regionId > 0)
                {
                    filters = new FilterCollection();
                    filters.Add("GeneralRegionMasterId", ProcedureFilterOperators.Like, regionId.ToString());
                }
                GeneralCityListResponse response = new GeneralCityClient().List(null, filters, null, 1, int.MaxValue);
                dropdownList.Add(new SelectListItem() { Text = "-------Select City-------" });
                GeneralCityListModel list = new GeneralCityListModel { GeneralCityList = response.GeneralCityList };
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Country.ToString()))
            {
                GeneralCountryListResponse response = new GeneralCountryClient().List(null, null, null, 1, int.MaxValue);
                if (response?.GeneralCountryList?.Count != 1)
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

            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Nationality.ToString()))
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

            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccessibleCentre.ToString()))
            {
                List<UserAccessibleCentreModel> accessibleCentreList = AccessibleCentreList();
                if (accessibleCentreList?.Count != 1)
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseDepartment.ToString()))
            {
                if (AccessibleCentreList()?.Count == 1 && string.IsNullOrEmpty(dropdownViewModel.Parameter))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Designation.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Department.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Organisation.ToString()))
            {
                OrganisationResponse item = new OrganisationClient().GetOrganisation();
                dropdownList.Add(new SelectListItem()
                {
                    Text = item.OrganisationModel.OrganisationName,
                    Value = item.OrganisationModel.OrganisationMasterId.ToString(),
                    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationModel.OrganisationMasterId)
                });
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.RegionalOffice.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.TaxGroup.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Region.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.ModuleList.ToString()))
            {
                UserModuleListResponse response = new UserClient().GetActiveModuleList();
                dropdownList.Add(new SelectListItem() { Text = "-------Select-------" });
                foreach (var item in response?.ModuleList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.ModuleName,
                        Value = Convert.ToString(item.UserModuleMasterId),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.UserModuleMasterId)
                    });
                }
            }
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.MenuList.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.MaritalStatus.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.BloodGroups.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.Occupation.ToString()))
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

            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.MeasurementUnit.ToString()))
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.FinancialYear.ToString()))
            {
                dropdownList.Add(new SelectListItem() { Value = "", Text = GeneralResources.SelectLabel });
                dropdownList.Add(new SelectListItem()
                {
                    Text = "2021",
                    Value = "2021",
                    Selected = "2021" == dropdownViewModel.DropdownSelectedValue
                });
                dropdownList.Add(new SelectListItem()
                {
                    Text = "2022",
                    Value = "2022",
                    Selected = "2022" == dropdownViewModel.DropdownSelectedValue
                });
                dropdownList.Add(new SelectListItem()
                {
                    Text = "2023",
                    Value = "2023",
                    Selected = "2023" == dropdownViewModel.DropdownSelectedValue
                });
            }

            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.CentrewiseBuilding.ToString()))
            {
                OrganisationCentrewiseBuildingListResponse response = new OrganisationCentrewiseBuildingClient().List(null, null, null, 1, int.MaxValue);
                OrganisationCentrewiseBuildingListModel list = new OrganisationCentrewiseBuildingListModel() { OrganisationCentrewiseBuildingList = response.OrganisationCentrewiseBuildingList };
                dropdownList.Add(new SelectListItem() { Text = "-------Select Building-------" });
                foreach (var item in list?.OrganisationCentrewiseBuildingList)
                {
                    dropdownList.Add(new SelectListItem()
                    {
                        Text = item.BuildName,
                        Value = item.OrganisationCentrewiseBuildingMasterId.ToString(),
                        Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationCentrewiseBuildingMasterId)
                    });
                }
            }

            dropdownViewModel.DropdownList = dropdownList;
            return dropdownViewModel;
        }

        private static string SpiltCentreCode(string centreCode)
        {
            centreCode = !string.IsNullOrEmpty(centreCode) && centreCode.Contains(":") ? centreCode.Split(':')[0] : centreCode;
            return centreCode;
        }
    }
}
