using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralPersonFollowUpController : BaseController
    {
        private readonly IGeneralPersonFollowUpAgent _generalPersonFollowUpAgent;
        private const string createEdit = "~/Views/GeneralPerson/GeneralPersonFollowUp/CreateEdit.cshtml";

        public GeneralPersonFollowUpController(IGeneralPersonFollowUpAgent generalPersonFollowUpAgent)
        {
            _generalPersonFollowUpAgent = generalPersonFollowUpAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralPersonFollowUpListViewModel list = _generalPersonFollowUpAgent.GetPersonFollowUpList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralPerson/GeneralPersonFollowUp/_List.cshtml", list);
            }
            return View($"~/Views/GeneralPerson/GeneralPersonFollowUp/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralPersonFollowUpViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralPersonFollowUpViewModel generalPersonFollowUpViewModel)
        {
            if (ModelState.IsValid)
            {
                generalPersonFollowUpViewModel = _generalPersonFollowUpAgent.CreatePersonFollowUp(generalPersonFollowUpViewModel);
                if (!generalPersonFollowUpViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralPersonFollowUpController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalPersonFollowUpViewModel.ErrorMessage));
            return View(createEdit, generalPersonFollowUpViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long generalPersonFollowUpId)
        {
            GeneralPersonFollowUpViewModel generalPersonFollowUpViewModel = _generalPersonFollowUpAgent.GetPersonFollowUp(generalPersonFollowUpId);
            return ActionView(createEdit, generalPersonFollowUpViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralPersonFollowUpViewModel generalPersonFollowUpViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalPersonFollowUpAgent.UpdatePersonFollowUp(generalPersonFollowUpViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalPersonFollowUpId = generalPersonFollowUpViewModel.GeneralPersonFollowUpId });
            }
            return View(createEdit, generalPersonFollowUpViewModel);
        }

        public virtual ActionResult Delete(string PersonFollowUpIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(PersonFollowUpIds))
            {
                status = _generalPersonFollowUpAgent.DeletePersonFollowUp(PersonFollowUpIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralPersonFollowUpController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralPersonFollowUpController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}