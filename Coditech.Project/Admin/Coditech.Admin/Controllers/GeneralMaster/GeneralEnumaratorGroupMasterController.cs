using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralEnumaratorGroupMasterController : BaseController
    {
        private readonly IGeneralEnumaratorGroupAgent _GeneralEnumaratorGroupAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralEnumaratorGroupMaster/CreateEdit.cshtml";

        public GeneralEnumaratorGroupMasterController(IGeneralEnumaratorGroupAgent GeneralEnumaratorGroupAgent)
        {
            _GeneralEnumaratorGroupAgent = GeneralEnumaratorGroupAgent;
        }

        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralEnumaratorGroupListViewModel list = _GeneralEnumaratorGroupAgent.GetEnumaratorGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralEnumaratorGroupMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralEnumaratorGroupMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralEnumaratorGroupViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                GeneralEnumaratorGroupViewModel = _GeneralEnumaratorGroupAgent.CreateEnumaratorGroup(GeneralEnumaratorGroupViewModel);
                if (!GeneralEnumaratorGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralEnumaratorGroupMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralEnumaratorGroupViewModel.ErrorMessage));
            return View(createEdit, GeneralEnumaratorGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short GeneralEnumaratorGroupId)
        {
            GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel = _GeneralEnumaratorGroupAgent.GetEnumaratorGroup(GeneralEnumaratorGroupId);
            return ActionView(createEdit, GeneralEnumaratorGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralEnumaratorGroupViewModel GeneralEnumaratorGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_GeneralEnumaratorGroupAgent.UpdateEnumaratorGroup(GeneralEnumaratorGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { GeneralEnumaratorGroupId = GeneralEnumaratorGroupViewModel.GeneralEnumaratorGroupMasterId });
            }
            return View(createEdit, GeneralEnumaratorGroupViewModel);
        }

        public virtual ActionResult Delete(string EnumaratorGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(EnumaratorGroupIds))
            {
                status = _GeneralEnumaratorGroupAgent.DeleteEnumaratorGroup(EnumaratorGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralEnumaratorGroupMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralEnumaratorGroupMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}