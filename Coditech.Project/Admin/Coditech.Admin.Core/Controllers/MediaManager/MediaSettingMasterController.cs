using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class MediaSettingMasterController : BaseController
    {
        private readonly IMediaSettingMasterAgent _mediaSettingMasterAgent;
        private const string createEdit = "~/Views/MediaManager/MediaSettingMaster/CreateEdit.cshtml";

        public MediaSettingMasterController(IMediaSettingMasterAgent mediaSettingMasterAgent)
        {
            _mediaSettingMasterAgent = mediaSettingMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            MediaSettingMasterListViewModel list = _mediaSettingMasterAgent.GetMediaSettingMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/MediaManager/MediaSettingMaster/_List.cshtml", list);
            }
            return View($"~/Views/MediaManager/MediaSettingMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Edit(byte mediaTypeMasterId)
        {
            MediaSettingMasterViewModel mediaSettingMasterViewModel = _mediaSettingMasterAgent.GetMediaSettingMaster(mediaTypeMasterId);
            return ActionView(createEdit, mediaSettingMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(MediaSettingMasterViewModel mediaSettingMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_mediaSettingMasterAgent.UpdateMediaSettingMaster(mediaSettingMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(mediaSettingMasterViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { mediaTypeMasterId = mediaSettingMasterViewModel.MediaTypeMasterId });
                }
                else if (string.Equals(mediaSettingMasterViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, mediaSettingMasterViewModel);
        }

        #region Protected

        #endregion
    }
}