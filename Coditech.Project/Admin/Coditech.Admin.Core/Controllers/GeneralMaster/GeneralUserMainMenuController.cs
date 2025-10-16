using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralUserMainMenuController : BaseController
    {
        private readonly IGeneralUserMainMenuAgent _generalUserMainMenuAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralUserMainMenu/CreateEdit.cshtml";

        public GeneralUserMainMenuController(IGeneralUserMainMenuAgent generalUserMainMenuAgent)
        {
            _generalUserMainMenuAgent = generalUserMainMenuAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            UserMainMenuListViewModel list = _generalUserMainMenuAgent.GetUserMainMenuList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralUserMainMenu/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralUserMainMenu/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new UserMainMenuViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(UserMainMenuViewModel generalUserMainnMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                generalUserMainnMenuViewModel = _generalUserMainMenuAgent.CreateUserMainMenu(generalUserMainnMenuViewModel);
                if (!generalUserMainnMenuViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalUserMainnMenuViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { userMainMenuMasterId = generalUserMainnMenuViewModel.UserMainMenuMasterId });
                    }
                    else if (string.Equals(generalUserMainnMenuViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalUserMainnMenuViewModel.ErrorMessage));
            return View(createEdit, generalUserMainnMenuViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short userMainMenuMasterId)
        {
            UserMainMenuViewModel generalUserMainnMenuViewModel = _generalUserMainMenuAgent.GetUserMainMenu(userMainMenuMasterId);
            return ActionView(createEdit, generalUserMainnMenuViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(UserMainMenuViewModel generalUserMainnMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalUserMainMenuAgent.UpdateUserMainMenu(generalUserMainnMenuViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalUserMainnMenuViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { userMainMenuMasterId = generalUserMainnMenuViewModel.UserMainMenuMasterId });
                }
                else if (string.Equals(generalUserMainnMenuViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalUserMainnMenuViewModel);
        }

        public virtual ActionResult Delete(string userMainMenuIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(userMainMenuIds))
            {
                status = _generalUserMainMenuAgent.DeleteUserMainMenu(userMainMenuIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralUserMainMenuController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralUserMainMenuController>(x => x.List(null));
        }
        public virtual ActionResult GetParentMemuCodeByModuleCode(string moduleCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.MenuList.ToString(),
                DropdownName = "ParentMenuCode",
                Parameter = moduleCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        #region Protected

        #endregion
    }
}