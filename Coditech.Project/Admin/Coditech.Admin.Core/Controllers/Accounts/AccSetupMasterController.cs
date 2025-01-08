using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccSetupMasterController : BaseController
    {
        private readonly IAccSetupMasterAgent _accSetupMasterAgent;
        private const string createEdit = "~/Views/Accounts/AccSetupMaster/CreateEdit.cshtml";

        public AccSetupMasterController(IAccSetupMasterAgent accSetupMasterAgent)
        {
            _accSetupMasterAgent = accSetupMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            AccSetupMasterListViewModel list = new AccSetupMasterListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _accSetupMasterAgent.GetAccSetupMasterList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccSetupMaster/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccSetupMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new AccSetupMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(AccSetupMasterViewModel accSetupMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                accSetupMasterViewModel = _accSetupMasterAgent.CreateAccSetupMaster(accSetupMasterViewModel);
                if (!accSetupMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable(accSetupMasterViewModel.CentreCode));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accSetupMasterViewModel.ErrorMessage));
            return View(createEdit, accSetupMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short accSetupMasterId)
        {
            AccSetupMasterViewModel accSetupMasterViewModel = _accSetupMasterAgent.GetAccSetupMaster(accSetupMasterId);
            return ActionView(createEdit, accSetupMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccSetupMasterViewModel accSetupMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                accSetupMasterViewModel = _accSetupMasterAgent.UpdateAccSetupMaster(accSetupMasterViewModel);
                SetNotificationMessage(accSetupMasterViewModel.HasError
                ? GetErrorNotificationMessage(accSetupMasterViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { accSetupMasterId = accSetupMasterViewModel.AccSetupMasterId });
            }
            return View(createEdit, accSetupMasterViewModel);
        }

       public virtual ActionResult Delete(string AccSetupMasterIds, string selectedCentreCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(AccSetupMasterIds))
            {
                status = _accSetupMasterAgent.DeleteAccSetupMaster(AccSetupMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = selectedCentreCode });
        }
        public virtual ActionResult Cancel(string SelectedCentreCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode };
            return RedirectToAction("List", dataTableViewModel);
        }

        #region Protected

        #endregion
    }
}