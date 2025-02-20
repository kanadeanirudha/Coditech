using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.API.Data;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralCurrencyMasterController : BaseController
    {
        IGeneralCurrencyMasterAgent _generalCurrencyMasterAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCurrencyMaster/CreateEdit.cshtml";
        public GeneralCurrencyMasterController(IGeneralCurrencyMasterAgent generalCurrencyMasterAgent)
        {
            _generalCurrencyMasterAgent = generalCurrencyMasterAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralCurrencyMasterListViewModel list = _generalCurrencyMasterAgent.GetCurrencyList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralCurrencyMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCurrencyMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralCurrencyMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                generalCurrencyMasterViewModel = _generalCurrencyMasterAgent.CreateCurrency(generalCurrencyMasterViewModel);
                if (!generalCurrencyMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralCurrencyMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalCurrencyMasterViewModel.ErrorMessage));
            return View(createEdit, generalCurrencyMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalCurrencyMasterId)
        {
            GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel = _generalCurrencyMasterAgent.GetCurrency(generalCurrencyMasterId);
            return ActionView(createEdit, generalCurrencyMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralCurrencyMasterViewModel generalCurrencyMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalCurrencyMasterAgent.UpdateCurrency(generalCurrencyMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalCurrencyMasterId = generalCurrencyMasterViewModel.GeneralCurrencyMasterId });
            }
            return View(createEdit, generalCurrencyMasterViewModel);
        }
        public virtual ActionResult Delete(string generalCurrencyMasterId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalCurrencyMasterId))
            {
                status = _generalCurrencyMasterAgent.DeleteCurrency(generalCurrencyMasterId, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCurrencyMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralCurrencyMasterController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}