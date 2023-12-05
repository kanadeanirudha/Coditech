using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralCountryMasterController : BaseController
    {
        private readonly IGeneralCountryAgent _generalCountryAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCountryMaster/CreateEdit.cshtml";

        public GeneralCountryMasterController(IGeneralCountryAgent generalCountryAgent)
        {
            _generalCountryAgent = generalCountryAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralCountryListViewModel list = _generalCountryAgent.GetCountryList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralCountryMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralCountryViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralCountryViewModel generalCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                generalCountryViewModel = _generalCountryAgent.CreateCountry(generalCountryViewModel);
                if (!generalCountryViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalCountryViewModel.ErrorMessage));
            return View(createEdit, generalCountryViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalCountryId)
        {
            GeneralCountryViewModel generalCountryViewModel = _generalCountryAgent.GetCountry(generalCountryId);
            return ActionView(createEdit, generalCountryViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralCountryViewModel generalCountryViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalCountryAgent.UpdateCountry(generalCountryViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalCountryId = generalCountryViewModel.GeneralCountryMasterId });
            }
            return View(createEdit, generalCountryViewModel);
        }

        public virtual ActionResult Delete(string countryIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(countryIds))
            {
                status = _generalCountryAgent.DeleteCountry(countryIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralCountryMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}