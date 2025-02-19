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
            AccGLSetupNarrationListViewModel list = new AccGLSetupNarrationListViewModel();
            GetListOnlyIfSingleCentre(dataTableModel);
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode))
            {
                list = _accGLSetupNarrationAgent.GetNarrationList(dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;


            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/Accounts/AccGLSetupNarration/_List.cshtml", list);
            }
            return View($"~/Views/Accounts/AccGLSetupNarration/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create(string centreCode , string narrationType)
        {
            AccGLSetupNarrationViewModel accGLSetupNarrationViewModel = new AccGLSetupNarrationViewModel()
            {
                CentreCode = centreCode,
                NarrationType =narrationType
            };
                return View(createEdit, accGLSetupNarrationViewModel);
      
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
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = accGLSetupNarrationViewModel.CentreCode  });
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
        public virtual ActionResult Cancel(string SelectedCentreCode, int accGLSetupNarrationId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode , SelectedParameter1 = accGLSetupNarrationId.ToString() };
            return RedirectToAction("List", dataTableViewModel);
        }
        #region Protected

        #endregion
    }
}