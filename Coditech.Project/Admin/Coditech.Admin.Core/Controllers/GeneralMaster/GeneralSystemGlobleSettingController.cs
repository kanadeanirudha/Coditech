using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralSystemGlobleSettingController : BaseController
    {
        private readonly IGeneralSystemGlobleSettingAgent _generalSystemGlobleSettingAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralSystemGlobleSettingMaster/CreateEdit.cshtml";

        public GeneralSystemGlobleSettingController(IGeneralSystemGlobleSettingAgent generalSystemGlobleSettingAgent)
        {
            _generalSystemGlobleSettingAgent = generalSystemGlobleSettingAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralSystemGlobleSettingListViewModel list = _generalSystemGlobleSettingAgent.GetSystemGlobleSettingList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralSystemGlobleSettingMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralSystemGlobleSettingMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralSystemGlobleSettingViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralSystemGlobleSettingViewModel generalSystemGlobleSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                generalSystemGlobleSettingViewModel = _generalSystemGlobleSettingAgent.CreateSystemGlobleSetting(generalSystemGlobleSettingViewModel);
                if (!generalSystemGlobleSettingViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalSystemGlobleSettingViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalSystemGlobleSettingId = generalSystemGlobleSettingViewModel.GeneralSystemGlobleSettingMasterId });
                    }
                    else if (string.Equals(generalSystemGlobleSettingViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalSystemGlobleSettingViewModel.ErrorMessage));
            return View(createEdit, generalSystemGlobleSettingViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalSystemGlobleSettingId)
        {
            GeneralSystemGlobleSettingViewModel generalSystemGlobleSettingViewModel = _generalSystemGlobleSettingAgent.GetSystemGlobleSetting(generalSystemGlobleSettingId);
            return ActionView(createEdit, generalSystemGlobleSettingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralSystemGlobleSettingViewModel generalSystemGlobleSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalSystemGlobleSettingAgent.UpdateSystemGlobleSetting(generalSystemGlobleSettingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalSystemGlobleSettingViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalSystemGlobleSettingId = generalSystemGlobleSettingViewModel.GeneralSystemGlobleSettingMasterId });
                }
                else if (string.Equals(generalSystemGlobleSettingViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalSystemGlobleSettingViewModel);
        }

        public virtual ActionResult Delete(string generalSystemGlobleSettingIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalSystemGlobleSettingIds))
            {
                status = _generalSystemGlobleSettingAgent.DeleteSystemGlobleSetting(generalSystemGlobleSettingIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralSystemGlobleSettingController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralSystemGlobleSettingController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}