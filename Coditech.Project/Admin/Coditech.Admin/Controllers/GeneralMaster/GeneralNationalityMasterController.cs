using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralNationalityMasterController : BaseController
    {
        private readonly IGeneralNationalityAgent _generalNationalityAgent;
         private const string createEdit = "~/Views/GeneralMaster/GeneralNationalityMaster/CreateEdit.cshtml";

        public GeneralNationalityMasterController(IGeneralNationalityAgent generalNationalityAgent)
        {
            _generalNationalityAgent = generalNationalityAgent;
        }

        public ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralNationalityListViewModel list = _generalNationalityAgent.GetNationalityList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralNationalityMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralNationalityMaster/List.cshtml", list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(createEdit, new GeneralNationalityViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralNationalityViewModel generalNationalityViewModel)
        {
            if (ModelState.IsValid)
            {
                generalNationalityViewModel = _generalNationalityAgent.CreateNationality(generalNationalityViewModel);
                if (!generalNationalityViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralNationalityMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalNationalityViewModel.ErrorMessage));
            return View(createEdit, generalNationalityViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short nationalityId)
        {
            GeneralNationalityViewModel generalNationalityViewModel = _generalNationalityAgent.GetNationality(nationalityId);
            return ActionView(createEdit, generalNationalityViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralNationalityViewModel generalNationalityViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalNationalityAgent.UpdateNationality(generalNationalityViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { nationalityId = generalNationalityViewModel.GeneralNationalityMasterId });
            }
            return View(createEdit, generalNationalityViewModel);
        }

        public virtual ActionResult Delete(string nationalityIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(nationalityIds))
            {
                status = _generalNationalityAgent.DeleteNationality(nationalityIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralNationalityMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralNationalityMasterController>(x => x.List(null));
        }
        #region Protected
        #endregion
    }
}