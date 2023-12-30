using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralEnumaratorMasterController : BaseController
    {
        private readonly IGeneralEnumaratorAgent _GeneralEnumaratorAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralEnumaratorMaster/CreateEdit.cshtml";

        public GeneralEnumaratorMasterController(IGeneralEnumaratorAgent GeneralEnumaratorAgent)
        {
            _GeneralEnumaratorAgent = GeneralEnumaratorAgent;
        }

        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralEnumaratorListViewModel list = _GeneralEnumaratorAgent.GetEnumaratorList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralEnumaratorMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralEnumaratorMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralEnumaratorViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralEnumaratorViewModel GeneralEnumaratorViewModel)
        {
            if (ModelState.IsValid)
            {
                GeneralEnumaratorViewModel = _GeneralEnumaratorAgent.CreateEnumarator(GeneralEnumaratorViewModel);
                if (!GeneralEnumaratorViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralEnumaratorMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralEnumaratorViewModel.ErrorMessage));
            return View(createEdit, GeneralEnumaratorViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int GeneralEnumaratorMasterId)
        {
            GeneralEnumaratorViewModel GeneralEnumaratorViewModel = _GeneralEnumaratorAgent.GetEnumarator(GeneralEnumaratorMasterId);
            return ActionView(createEdit, GeneralEnumaratorViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralEnumaratorViewModel GeneralEnumaratorViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_GeneralEnumaratorAgent.UpdateEnumarator(GeneralEnumaratorViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { GeneralEnumaratorMasterId = GeneralEnumaratorViewModel.GeneralEnumaratorGroupId });
            }
            return View(createEdit, GeneralEnumaratorViewModel);
        }

        public virtual ActionResult Delete(string enumaratorIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(enumaratorIds))
            {
                status = _GeneralEnumaratorAgent.DeleteEnumarator(enumaratorIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralEnumaratorMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralEnumaratorMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}