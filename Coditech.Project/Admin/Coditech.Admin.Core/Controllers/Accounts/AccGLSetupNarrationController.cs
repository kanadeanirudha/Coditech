using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class AccGLSetupNarrationController : BaseController
    {
        private readonly IAccGLSetupNarrationAgent _accGLSetupNarrationAgent;
        private const string createEdit = "~/Views/Accounts/AccGLSetupNarration/CreateEdit.cshtml";

        public AccGLSetupNarrationController(IAccGLSetupNarrationAgent accGLSetupNarrationAgent)
        {
            _accGLSetupNarrationAgent = accGLSetupNarrationAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            AccGLSetupNarrationListViewModel list = _accGLSetupNarrationAgent.GetNarrationList(dataTableModel);

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccGLSetupNarration/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccGLSetupNarration/List.cshtml", list);
        } 

        [HttpGet]
        public virtual ActionResult Create()
        
        {
            return View(createEdit, new AccGLSetupNarrationViewModel());
        }


        [HttpPost]
        public virtual ActionResult Create(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel)
        {
            if (ModelState.IsValid)
            {
                accGLSetupNarrationViewModel = _accGLSetupNarrationAgent.CreateNarration(accGLSetupNarrationViewModel);
                if (!accGLSetupNarrationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(accGLSetupNarrationViewModel.ErrorMessage));
            return View(createEdit, accGLSetupNarrationViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int accGLSetupNarrationId)
        {
            AccGLSetupNarrationViewModel accGLSetupNarrationViewModel = _accGLSetupNarrationAgent.GetNarration(accGLSetupNarrationId);
            return ActionView(createEdit, accGLSetupNarrationViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(AccGLSetupNarrationViewModel accGLSetupNarrationViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_accGLSetupNarrationAgent.UpdateNarration(accGLSetupNarrationViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { accGLSetupNarrationId = accGLSetupNarrationViewModel.AccGLSetupNarrationId });
            }
            return View(createEdit, accGLSetupNarrationViewModel);
        }

        public virtual ActionResult Delete(string narrationIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(narrationIds))
            {
                status = _accGLSetupNarrationAgent.DeleteNarration(narrationIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<AccGLSetupNarrationController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<AccGLSetupNarrationController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}