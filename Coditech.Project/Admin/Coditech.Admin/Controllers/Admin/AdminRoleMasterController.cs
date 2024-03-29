﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Controllers
{
    public class AdminRoleMasterController : BaseController
    {
        private readonly IAdminRoleMasterAgent _adminRoleMasterAgent;
        public AdminRoleMasterController(IAdminRoleMasterAgent adminRoleMasterAgent)
        {
            _adminRoleMasterAgent = adminRoleMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            AdminRoleListViewModel list = new AdminRoleListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && dataTableViewModel.SelectedDepartmentId > 0)
            {
                list = _adminRoleMasterAgent.GetAdminRoleList(dataTableViewModel);
            }

            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableViewModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Admin/AdminRoleMaster/_List.cshtml", list);
            }
            return View($"~/Views/Admin/AdminRoleMaster/List.cshtml", list);
        }
        public virtual ActionResult Edit(int adminRoleMasterId)
        {
            AdminRoleViewModel adminRoleViewModel = _adminRoleMasterAgent.GetAdminRoleDetailsById(adminRoleMasterId);
            BindDropdown(adminRoleViewModel);
            return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AdminRoleViewModel adminRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _adminRoleMasterAgent.UpdateAdminRole(adminRoleViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = adminRoleViewModel.SelectedCentreCode, SelectedDepartmentId = Convert.ToInt32(adminRoleViewModel.SelectedDepartmentId) });
                }
            }
            BindDropdown(adminRoleViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(adminRoleViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleViewModel);
        }
        public virtual ActionResult AllocateAccessRights(int adminRoleMasterId)
        {
            AdminRoleViewModel adminRoleViewModel = _adminRoleMasterAgent.GetAdminRoleDetailsById(adminRoleMasterId);
            BindDropdown(adminRoleViewModel);
            return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleViewModel);
        }

        public virtual ActionResult Cancel(string SelectedCentreCode, int SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }

        #region Protected
        protected virtual void BindDropdown(AdminRoleViewModel adminRoleViewModel)
        {
            adminRoleViewModel.MonitoringLevelList = new List<SelectListItem>();
            adminRoleViewModel.MonitoringLevelList.Add(new SelectListItem { Text = AdminConstants.Self, Value = AdminConstants.Self });
            if (adminRoleViewModel?.AllCentreList?.Count > 1)
            {
                adminRoleViewModel.MonitoringLevelList.Add(new SelectListItem { Text = AdminConstants.Other, Value = AdminConstants.Other });
            }

            if (adminRoleViewModel?.AllCentreList == null || adminRoleViewModel?.AllCentreList?.Count == 0)
            {
                adminRoleViewModel.AllCentreList = adminRoleViewModel.AllCentreList;
                adminRoleViewModel.AdminRoleCode = adminRoleViewModel.AdminRoleCode;
            }
            adminRoleViewModel.SelectedCentreNameForSelf = adminRoleViewModel.AllCentreList?.FirstOrDefault(x => x.CentreCode == adminRoleViewModel.SelectedCentreCodeForSelf)?.CentreName;
            adminRoleViewModel?.AllCentreList?.RemoveAll(x => x.CentreCode == adminRoleViewModel.SelectedCentreCodeForSelf);
        }
        #endregion
    }
}