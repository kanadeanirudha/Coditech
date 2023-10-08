using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Client;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;

using Microsoft.AspNetCore.Mvc.Rendering;
using RARIndia.Model.Model;

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
            if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.City.ToString()))
            {
                GeneralCityListResponse response = new GeneralCityClient().List(null, null, null, 1, int.MaxValue);
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select City-------" });
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
            else if (Equals(dropdownViewModel.DropdownType, DropdownTypeEnum.AccessibleCentre.ToString()))
            {
                List<UserAccessibleCentreModel> accessibleCentreList = AccessibleCentreList();
                if (accessibleCentreList?.Count > 1)
                    dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Centre-------" });
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
                   //GeneralDepartmentListResponse response = new GeneralDepartmentClient().GetDepartmentsByCentreCode(centreCode);
                   // list = new GeneralDepartmentListModel { GeneralDepartmentList = response?.GeneralDepartmentList };
                }
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Department-------" });
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
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Designation-------" });
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
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Department-------" });
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
                //OrganisationMasterViewModel item
                //    = new OrganisationMasterBA().GetOrganisationDetails();
                //dropdownList.Add(new SelectListItem()
                //{
                //    Text = item.OrganisationName,
                //    Value = item.OrganisationMasterId.ToString(),
                //    Selected = dropdownViewModel.DropdownSelectedValue == Convert.ToString(item.OrganisationMasterId)
                //});
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
                dropdownList.Add(new SelectListItem() { Value = "", Text = "-------Select Tax Group-------" });
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
