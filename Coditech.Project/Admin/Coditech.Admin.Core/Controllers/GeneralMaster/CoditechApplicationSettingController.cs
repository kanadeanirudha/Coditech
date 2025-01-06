using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class CoditechApplicationSettingController : BaseController
    {
        private readonly ICoditechApplicationSettingAgent _coditechApplicationSettingAgent;
        private const string createEdit = "~/Views/GeneralMaster/CoditechApplicationSetting/CreateEdit.cshtml";

        public CoditechApplicationSettingController(ICoditechApplicationSettingAgent coditechApplicationSettingAgent)
        {
            _coditechApplicationSettingAgent = coditechApplicationSettingAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            CoditechApplicationSettingListViewModel list = _coditechApplicationSettingAgent.GetCoditechApplicationSettingList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/CoditechApplicationSetting/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/CoditechApplicationSetting/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new CoditechApplicationSettingViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(CoditechApplicationSettingViewModel coditechApplicationSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                coditechApplicationSettingViewModel = _coditechApplicationSettingAgent.CreateCoditechApplicationSetting(coditechApplicationSettingViewModel);
                if (!coditechApplicationSettingViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<CoditechApplicationSettingController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(coditechApplicationSettingViewModel.ErrorMessage));
            return View(createEdit, coditechApplicationSettingViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short coditechApplicationSettingId)
        {
            CoditechApplicationSettingViewModel coditechApplicationSettingViewModel = _coditechApplicationSettingAgent.GetCoditechApplicationSetting(coditechApplicationSettingId);
            return ActionView(createEdit, coditechApplicationSettingViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(CoditechApplicationSettingViewModel coditechApplicationSettingViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_coditechApplicationSettingAgent.UpdateCoditechApplicationSetting(coditechApplicationSettingViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { coditechApplicationSettingId = coditechApplicationSettingViewModel.CoditechApplicationSettingId });
            }
            return View(createEdit, coditechApplicationSettingViewModel);
        }

        public virtual ActionResult Delete(string coditechApplicationSettingIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(coditechApplicationSettingIds))
            {
                status = _coditechApplicationSettingAgent.DeleteCoditechApplicationSetting(coditechApplicationSettingIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<CoditechApplicationSettingController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<CoditechApplicationSettingController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}