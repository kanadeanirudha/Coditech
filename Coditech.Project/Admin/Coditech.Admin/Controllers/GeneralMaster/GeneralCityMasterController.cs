using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralCityMasterController : BaseController
    {
        private readonly IGeneralCityAgent _generalCityAgent;

        private const string createEdit = "~/Views/GeneralMaster/GeneralCityMaster/CreateEdit.cshtml";

        public GeneralCityMasterController(IGeneralCityAgent generalCityAgent)
        {
            _generalCityAgent = generalCityAgent;

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
            BindDropDown(generalCityViewModel);
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
            BindDropDown(generalCityViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(generalCityViewModel.ErrorMessage));
            return View(createEdit, generalCityViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int cityId)
        {
            GeneralCityViewModel generalCityViewModel = _generalCityAgent.GetCity(cityId);
            BindDropDown(generalCityViewModel);
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
            BindDropDown(generalCityViewModel);
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
        #region Private
        protected void BindDropDown(GeneralCityViewModel generalCityViewModel)
        {
            generalCityViewModel.AllCityList = _generalCityAgent.GetAllCityList().GeneralCityList;
        }
        #endregion
    }
}