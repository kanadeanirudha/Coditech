using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralCityMasterController : BaseController
    {
         IGeneralCityAgent _generalCityAgent;
        IGeneralCountryAgent _generalCountryAgent = null;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCityMaster/CreateEdit.cshtml";
        public GeneralCityMasterController(IGeneralCityAgent generalCityAgent, IGeneralCountryAgent generalCountryAgent)
        {
            _generalCityAgent = generalCityAgent;
            _generalCountryAgent = generalCountryAgent;
        }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralCityListViewModel list = _generalCityAgent.GetCityList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralCityMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCityMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralCityViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralCityViewModel generalCityViewModel)
        {
            if (ModelState.IsValid)
            {
                generalCityViewModel = _generalCityAgent.CreateCity(generalCityViewModel);
                if (!generalCityViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalCityViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { cityId = generalCityViewModel.GeneralCityMasterId });
                    }
                    else if (string.Equals(generalCityViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalCityViewModel.ErrorMessage));
            return View(createEdit, generalCityViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int cityId)
        {
            GeneralCityViewModel generalCityViewModel = _generalCityAgent.GetCity(cityId);
            return ActionView(createEdit, generalCityViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralCityViewModel generalCityViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalCityAgent.UpdateCity(generalCityViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalCityViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { cityId = generalCityViewModel.GeneralCityMasterId });
                }
                else if (string.Equals(generalCityViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalCityViewModel);
        }
        public virtual ActionResult Delete(string cityIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(cityIds))
            {
                status = _generalCityAgent.DeleteCity(cityIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}