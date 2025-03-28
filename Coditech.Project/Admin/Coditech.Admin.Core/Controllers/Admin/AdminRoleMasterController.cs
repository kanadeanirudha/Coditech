﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
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
                    // Redirect back to the Edit action with the same adminRoleMasterId
                    return RedirectToAction("Edit", new { adminRoleMasterId = adminRoleViewModel.AdminRoleMasterId });
                }
            }

            BindDropdown(adminRoleViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(adminRoleViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminRoleMaster/Edit.cshtml", adminRoleViewModel);
        }

        public virtual ActionResult AllocateAccessRights(int adminRoleMasterId, string moduleCode = null)
        {
            AdminRoleMenuDetailsViewModel adminRoleMenuDetailsViewModel = _adminRoleMasterAgent.GetAdminRoleMenuDetailsById(adminRoleMasterId, moduleCode);
            return View("~/Views/Admin/AdminRoleMaster/AllocateAccessRights.cshtml", adminRoleMenuDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult AllocateAccessRights(AdminRoleMenuDetailsViewModel adminRoleMenuDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _adminRoleMasterAgent.InsertUpdateAdminRoleMenuDetails(adminRoleMenuDetailsViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    return RedirectToAction("AllocateAccessRights", new { adminRoleMasterId = adminRoleMenuDetailsViewModel.AdminRoleMasterId, moduleCode = adminRoleMenuDetailsViewModel.ModuleCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(adminRoleMenuDetailsViewModel.ErrorMessage));
            return RedirectToAction("AllocateAccessRights", new { adminRoleMasterId = adminRoleMenuDetailsViewModel.AdminRoleMasterId, moduleCode = adminRoleMenuDetailsViewModel.ModuleCode });
        }

        public virtual ActionResult Cancel(string SelectedCentreCode, short SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }

        public virtual ActionResult RoleAllocatedToUserList(DataTableViewModel dataTableModel)
        {
            AdminRoleApplicableDetailsListViewModel list = _adminRoleMasterAgent.RoleAllocatedToUserList(Convert.ToInt32(dataTableModel.SelectedParameter1), dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Admin/AdminRoleMaster/_RoleAllocatedToUserList.cshtml", list);
            }
            return View($"~/Views/Admin/AdminRoleMaster/RoleAllocatedToUserList.cshtml", list);
        }

        public virtual ActionResult GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId)
        {
            AdminRoleApplicableDetailsViewModel adminRoleApplicableDetailsViewModel = _adminRoleMasterAgent.GetAssociateUnAssociateAdminRoleToUser(adminRoleMasterId, adminRoleApplicableDetailId);
            List<SelectListItem> employeeList = new List<SelectListItem>();
            if (adminRoleApplicableDetailsViewModel.EmployeeId == 0)
                employeeList.Add(new SelectListItem { Text = "--------Select--------", Value = "" });

            foreach (EmployeeMasterModel item in adminRoleApplicableDetailsViewModel?.EmployeeList)
            {
                employeeList.Add(new SelectListItem { Text = !string.IsNullOrEmpty(item.FullNameWithPersonCode) ? item.FullNameWithPersonCode : item.FullName, Value = item.EmployeeId.ToString() });
            }

            ViewData["EmployeeList"] = employeeList;
            return View("~/Views/Admin/AdminRoleMaster/AssociateUnAssociateAdminRoleToUser.cshtml", adminRoleApplicableDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult AssociateUnAssociateAdminRoleToUser(AdminRoleApplicableDetailsViewModel adminRoleApplicableDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _adminRoleMasterAgent.AssociateUnAssociateAdminRoleToUser(adminRoleApplicableDetailsViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedParameter1 = Convert.ToString(adminRoleApplicableDetailsViewModel.AdminRoleMasterId) };
                    return RedirectToAction("RoleAllocatedToUserList", dataTableViewModel);
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(adminRoleApplicableDetailsViewModel.ErrorMessage));
            return View("~/Views/Admin/AdminRoleMaster/AssociateUnAssociateAdminRoleToUser.cshtml", adminRoleApplicableDetailsViewModel);
        }

        public virtual ActionResult RoleWiseFolderAction(int adminRoleMasterId)
        {
            AdminRoleMediaFolderActionViewModel adminRoleMediaFolderActionViewModel = _adminRoleMasterAgent.GetAdminRoleWiseMediaFolderActionById(adminRoleMasterId);
            adminRoleMediaFolderActionViewModel.MediaActionList = new List<SelectListItem>();
            var mediaFolderActionList = Enum.GetValues(typeof(MediaFolderActionEnum)).Cast<MediaFolderActionEnum>().ToList();
            foreach (var item in mediaFolderActionList)
            {
                adminRoleMediaFolderActionViewModel.MediaActionList.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }
            return View("~/Views/Admin/AdminRoleMaster/RoleWiseFolderAction.cshtml", adminRoleMediaFolderActionViewModel);
        }

        [HttpPost]
        public virtual ActionResult RoleWiseFolderAction(AdminRoleMediaFolderActionViewModel adminRoleMediaFolderActionViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_adminRoleMasterAgent.InsertUpdateAdminRoleWiseMediaFolderAction(adminRoleMediaFolderActionViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("RoleWiseFolderAction", new { adminRoleMasterId = adminRoleMediaFolderActionViewModel.AdminRoleMasterId });
            }
            return View("~/Views/Admin/AdminRoleMaster/RoleWiseFolderAction.cshtml", adminRoleMediaFolderActionViewModel);
        }

        public virtual ActionResult RoleWiseFolderAccess(int adminRoleMasterId)
        {
            AdminRoleMediaFoldersViewModel adminRoleMediaFoldersViewModel = _adminRoleMasterAgent.GetAdminRoleWiseMediaFoldersById(adminRoleMasterId);
            adminRoleMediaFoldersViewModel.TreeViewJson = Newtonsoft.Json.JsonConvert.SerializeObject(adminRoleMediaFoldersViewModel.TreeViewList);
            return View("~/Views/Admin/AdminRoleMaster/RoleWiseFolders.cshtml", adminRoleMediaFoldersViewModel);
        }

        [HttpPost]
        public virtual ActionResult RoleWiseFolderAccess(AdminRoleMediaFoldersViewModel adminRoleMediaFoldersViewModel)
        {
            if (ModelState.IsValid)
            {
                bool status = _adminRoleMasterAgent.InsertUpdateAdminRoleWiseMediaFolders(adminRoleMediaFoldersViewModel).HasError;
                SetNotificationMessage(status
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));

                if (!status)
                {
                    return RedirectToAction("RoleWiseFolderAccess", new { adminRoleMasterId = adminRoleMediaFoldersViewModel.AdminRoleMasterId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(adminRoleMediaFoldersViewModel.ErrorMessage));
            return RedirectToAction("RoleWiseFolderAccess", new { adminRoleMasterId = adminRoleMediaFoldersViewModel.AdminRoleMasterId });
        }

        #region Protected
        protected virtual void BindDropdown(AdminRoleViewModel adminRoleViewModel)
        {
            if (adminRoleViewModel != null)
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
        }
        #endregion
    }
}