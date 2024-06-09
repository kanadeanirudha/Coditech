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
        public virtual ActionResult Create()
        {
            return View(createEdit, new MediaSettingMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(MediaSettingMasterViewModel mediaSettingMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                mediaSettingMasterViewModel = _mediaSettingMasterAgent.CreateMediaSettingMaster(mediaSettingMasterViewModel);
                if (!mediaSettingMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(mediaSettingMasterViewModel.ErrorMessage));
            return View(createEdit, mediaSettingMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short mediaSettingMasterId)
        {
            MediaSettingMasterViewModel mediaSettingMasterViewModel = _mediaSettingMasterAgent.GetMediaSettingMaster(mediaSettingMasterId);
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
                return RedirectToAction("Edit", new { mediaSettingMasterId = mediaSettingMasterViewModel.MediaSettingMasterId });
            }
            return View(createEdit, mediaSettingMasterViewModel);
        }

        public virtual ActionResult Delete(string MediaSettingMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(MediaSettingMasterIds))
            {
                status = _mediaSettingMasterAgent.DeleteMediaSettingMaster(MediaSettingMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<MediaSettingMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<MediaSettingMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}