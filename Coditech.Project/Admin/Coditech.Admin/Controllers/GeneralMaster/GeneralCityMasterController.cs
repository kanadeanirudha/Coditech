using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralCityListViewModel list = _generalCityAgent.GetCityList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralCityMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCityMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GeneralCityViewModel generalCityViewModel = new GeneralCityViewModel();
            BindDropdown(generalCityViewModel);
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
                    return RedirectToAction<GeneralCityMasterController>(x => x.List(null));
                }
            }
            BindDropdown(generalCityViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(generalCityViewModel.ErrorMessage));
            return View(createEdit, generalCityViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int cityId)
        {
            GeneralCityViewModel generalCityViewModel = _generalCityAgent.GetCity(cityId);
            BindDropdown(generalCityViewModel);
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
                return RedirectToAction("Edit", new { cityId = generalCityViewModel.GeneralCityMasterId });
            }
            BindDropdown(generalCityViewModel);
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
        protected virtual void BindDropdown(GeneralCityViewModel generalCityViewModel)
        {
            generalCityViewModel.CountryList = _generalCountryAgent.GetCountryList().GeneralCountryList;
        }
        #endregion
    }
}