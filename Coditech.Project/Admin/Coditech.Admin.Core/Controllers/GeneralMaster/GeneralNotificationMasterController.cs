using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;
namespace Coditech.Admin.Controllers
{
    public class GeneralNotificationMasterController : BaseController
    {
        private readonly IGeneralNotificationAgent _generalNotificationAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralNotificationMaster/CreateEdit.cshtml";

      public GeneralNotificationMasterController(IGeneralNotificationAgent generalNotificationAgent)
      {
            _generalNotificationAgent = generalNotificationAgent;
        
      }
        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralNotificationListViewModel list = _generalNotificationAgent.GetNotificationList(dataTableModel);
            if(AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralNotificationMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralNotificationMaster/List.cshtml", list);

        }
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralNotificationViewModel());
        }
        [HttpPost]
        public virtual ActionResult Create(GeneralNotificationViewModel generalNotificationViewModel)
        {
            if (ModelState.IsValid)
            {
                generalNotificationViewModel = _generalNotificationAgent.CreateNotification(generalNotificationViewModel);
                if (!generalNotificationViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalNotificationViewModel.ErrorMessage));
            return View(createEdit, generalNotificationViewModel);

        }
        public virtual ActionResult Edit(long GeneralNotificationId)
        {
            GeneralNotificationViewModel generalNotificationViewModel = _generalNotificationAgent.GetNotification(GeneralNotificationId);
            return ActionView(createEdit, generalNotificationViewModel);
        }
        [HttpPost]
        public virtual ActionResult Edit(GeneralNotificationViewModel generalNotificationViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalNotificationAgent.UpdateNotification(generalNotificationViewModel).HasError
                 ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                 : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { GeneralNotificationId = generalNotificationViewModel.GeneralNotificationId });
            }
            return View(createEdit, generalNotificationViewModel);
        }
        public virtual ActionResult Delete(string GeneralNotificationId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(GeneralNotificationId))
            {
                status = _generalNotificationAgent.DeleteNotification(GeneralNotificationId, out message);
                SetNotificationMessage(!status
                    ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                    : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralNotificationMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralNotificationMasterController>(x => x.List(null));
        }

        #region Protected
        #endregion


    }
}
