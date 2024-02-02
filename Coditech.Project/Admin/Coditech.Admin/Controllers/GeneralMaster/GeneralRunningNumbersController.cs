using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralRunningNumbersController : BaseController
    {
        private readonly IGeneralRunningNumbersAgent _generalRunningNumbersAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralRunningNumbers/CreateEdit.cshtml";
        public GeneralRunningNumbersController(IGeneralRunningNumbersAgent generalRunningNumbersAgent)
        {
            _generalRunningNumbersAgent = generalRunningNumbersAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            GeneralRunningNumbersListViewModel list = new GeneralRunningNumbersListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode))
            {
                list = _generalRunningNumbersAgent.GetRunningNumbersList(dataTableViewModel);
            }
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralRunningNumbers/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralRunningNumbers/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralRunningNumbersViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralRunningNumbersViewModel generalRunningNumbersViewModel)
        {
            if (ModelState.IsValid)
            {
                generalRunningNumbersViewModel = _generalRunningNumbersAgent.CreateRunningNumbers(generalRunningNumbersViewModel);
                if (!generalRunningNumbersViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralRunningNumbersController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalRunningNumbersViewModel.ErrorMessage));
            return View(createEdit, generalRunningNumbersViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long generalRunningNumberId)
        {
            GeneralRunningNumbersViewModel generalRunningNumbersViewModel = _generalRunningNumbersAgent.GetRunningNumbers(generalRunningNumberId);
            return ActionView(createEdit, generalRunningNumbersViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralRunningNumbersViewModel generalRunningNumbersViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalRunningNumbersAgent.UpdateRunningNumbers(generalRunningNumbersViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalRunningNumberId = generalRunningNumbersViewModel.GeneralRunningNumberId });
            }
            return View(createEdit, generalRunningNumbersViewModel);
        }

        public virtual ActionResult Delete(string generalRunningNumberIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalRunningNumberIds))
            {
                status = _generalRunningNumbersAgent.DeleteRunningNumbers(generalRunningNumberIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralRunningNumbersController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralRunningNumbersController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}